using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Fighting
{
    /// <summary>
    /// Логика взаимодействия для FightWindow.xaml
    /// </summary>
    public partial class FightWindow : Window
    {
        Fighter FirstUnit;
        Fighter SecondUnit;
        int FirstStep = 1;
        int SecondStep = 0;
        double FirstExp;
        double SecondExp;
        public FightWindow()
        {
            InitializeComponent();
            foreach (var item in FighterService.FighterList)
            {
                cb_FirstPlayer.Items.Add(item.Name);
            }
            foreach (var item in FighterService.FighterList)
            {
                cb_SecondPlayer.Items.Add(item.Name);
            }
        }

        private void cb_FirstPlayer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var FirstFighter in FighterService.FighterList)
            {
                if (FirstFighter.Name == cb_FirstPlayer.SelectedItem.ToString())
                {
                    FirstUnit = new Fighter(FirstFighter.Name, FirstFighter.Strength, FirstFighter.Dexterity, FirstFighter.Luck, FirstFighter.Constitution, FirstFighter.Intelligence);
                    pb_FirstPlayerHP.Value= FirstUnit.HP;
                    pb_FirstPlayerHP.Maximum = FirstUnit.HP;
                    pb_FirstPlayerHP.Minimum = 0;
                    pb_FirstPlayerMana.Value = FirstUnit.Mana;
                    pb_FirstPlayerMana.Maximum = FirstUnit.Mana;
                    pb_FirstPlayerMana.Minimum = 0;
                    lb_FirstPlayerPhAttackValue.Content = FirstUnit.PhAttack;
                    lb_FirstPlayerMageAttackValue.Content = FirstUnit.MageAttack;
                    lb_FirstPlayer_HPValue.Content = $"{FirstUnit.HP}/{pb_FirstPlayerHP.Maximum}";
                    lb_FirstPlayer_ManaValue.Content = $"{FirstUnit.Mana}/{pb_FirstPlayerMana.Maximum}";           
                    cb_SecondPlayer.Items.Remove(FirstFighter.Name);
                    cb_SecondPlayer.IsEnabled = true;
                    cb_FirstPlayer.IsEnabled = false;
                    FirstUnit.Point = FirstFighter.Point;
                }
            }  
        }

        private void cb_SecondPlayer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var SecondFighter in FighterService.FighterList)
            {
                if (SecondFighter.Name == cb_SecondPlayer.SelectedItem.ToString())
                {
                    SecondUnit = new Fighter(SecondFighter.Name, SecondFighter.Strength, SecondFighter.Dexterity, SecondFighter.Luck, SecondFighter.Constitution, SecondFighter.Intelligence);
                    pb_SecondPlayerHP.Value = SecondUnit.HP;
                    pb_SecondPlayerHP.Maximum = SecondUnit.HP;
                    pb_SecondPlayerHP.Minimum = 0;
                    pb_SecondPlayerMana.Value = SecondUnit.Mana;
                    pb_SecondPlayerMana.Maximum = SecondUnit.Mana;
                    pb_SecondPlayerMana.Minimum = 0;
                    lb_SecondPlayerPhAttackValue.Content = SecondUnit.PhAttack;
                    lb_SecondPlayer_HPValue.Content = $"{SecondUnit.HP}/{pb_SecondPlayerHP.Maximum}";
                    lb_SecondPlayer_ManaValue.Content = $"{SecondUnit.Mana}/{pb_SecondPlayerMana.Maximum}";
                    SecondUnit.Evasion = ((SecondUnit.Dexterity - FirstUnit.Dexterity) * 0.1) * 100;
                    FirstUnit.Evasion = ((FirstUnit.Dexterity - SecondUnit.Dexterity) * 0.1) * 100;
                    SecondUnit.BlockBreak = ((SecondUnit.Strength - FirstUnit.Strength) * 0.05) * 100;
                    FirstUnit.BlockBreak = ((FirstUnit.Strength - SecondUnit.Strength) * 0.05) * 100;
                    SecondUnit.CrtChance = ((SecondUnit.Luck - FirstUnit.Luck * 0.5) * 0.1) * 100;
                    FirstUnit.CrtChance = ((FirstUnit.Luck - SecondUnit.Luck * 0.5) * 0.1) * 100;
                    lb_SecondPlayerEvasionValue.Content = $"{SecondUnit.Evasion}%";
                    lb_FirstPlayerEvasionValue.Content = $"{FirstUnit.Evasion}%";
                    lb_SecondPlayerCrtChanceValue.Content = $"{SecondUnit.CrtChance}%";
                    lb_FirstPlayerCrtChanceValue.Content = $"{FirstUnit.CrtChance}%";
                    lb_SecondPlayerBlockBreakValue.Content = $"{SecondUnit.BlockBreak}%";
                    lb_FirstPlayerBlockBreakValue.Content = $"{FirstUnit.BlockBreak}%";
                    lb_SecondPlayerMageAttackValue.Content = SecondUnit.MageAttack;
                    SecondUnit.Point = SecondFighter.Point;
                    if (SecondUnit.Evasion < 0)
                    {
                        SecondUnit.Evasion = 0;
                        lb_SecondPlayerEvasionValue.Content = "0%";
                    }
                    
                    if (FirstUnit.Evasion < 0)
                    {
                        FirstUnit.Evasion = 0;
                        lb_FirstPlayerEvasionValue.Content = "0%";
                    }
                    
                    if (SecondUnit.CrtChance < 0)
                    {
                        SecondUnit.CrtChance = 0;
                        lb_SecondPlayerCrtChanceValue.Content = "0%";
                    }

                    if (FirstUnit.CrtChance < 0)
                    {
                        FirstUnit.CrtChance = 0;
                        lb_FirstPlayerCrtChanceValue.Content = "0%";
                    }

                    if (SecondUnit.BlockBreak < 0)
                    {
                        SecondUnit.BlockBreak = 0;
                        lb_SecondPlayerBlockBreakValue.Content = "0%";
                    }

                    if (FirstUnit.BlockBreak < 0)
                    {
                        FirstUnit.BlockBreak = 0;
                        lb_FirstPlayerBlockBreakValue.Content = "0%";
                    }
                    cb_FirstPlayer.Items.Remove(SecondFighter.Name);
                    PhAtt_button.Visibility = Visibility.Visible;
                    MageAtt_button.Visibility = Visibility.Visible;
                    Heal_button.Visibility = Visibility.Visible;
                    lb_Step.Content = $"Ход игрока {FirstUnit.Name}!";
                }
            }
            cb_SecondPlayer.IsEnabled = false;
        }

        private void PhAtt_button_Click(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            if (FirstStep == 1)
            {
                if (cb_FirstPlayerAttack.SelectedItem != null && cb_FirstPlayerBlock.SelectedItem != null && cb_SecondPlayerAttack.SelectedItem != null && cb_SecondPlayerBlock.SelectedItem != null)
                {
                    if (cb_FirstPlayerAttack.SelectedItem.ToString() != cb_SecondPlayerBlock.SelectedItem.ToString())
                    {
                        if (rnd.Next(0, 101) > SecondUnit.Evasion)
                        {
                            if (rnd.Next(1, 101) < FirstUnit.CrtChance)
                            {
                                if (FirstUnit.HP == 0)
                                {
                                    MessageBox.Show($"Победил {SecondUnit.Name}!");
                                    SecondUnit.Exp += SecondExp * 3 * FirstUnit.Lvl;
                                    FirstUnit.Exp += FirstExp;
                                    if (SecondUnit.Exp >= 100 && SecondUnit.Exp < 300)
                                    {
                                        SecondUnit.Lvl = 1;
                                        SecondUnit.Point += 2;
                                    }
                                    if (SecondUnit.Exp >= 300 && SecondUnit.Exp < 800)
                                    {
                                        SecondUnit.Lvl = 2;
                                        SecondUnit.Point += 3;
                                    }
                                    if (SecondUnit.Exp >= 800 && SecondUnit.Exp < 1800)
                                    {
                                        SecondUnit.Lvl = 3;
                                        SecondUnit.Point += 2;
                                    }
                                    if (SecondUnit.Exp >= 800 && SecondUnit.Exp < 1800)
                                    {
                                        SecondUnit.Lvl = 4;
                                        SecondUnit.Point += 2;
                                    }
                                    if (SecondUnit.Exp >= 1800 && SecondUnit.Exp < 3000)
                                    {
                                        SecondUnit.Lvl = 5;
                                        SecondUnit.Point += 5;
                                    }
                                    if (FirstUnit.Exp >= 100 && FirstUnit.Exp < 300)
                                    {
                                        FirstUnit.Lvl = 1;
                                        FirstUnit.Point += 2;
                                    }
                                    if (FirstUnit.Exp >= 300 && FirstUnit.Exp < 800)
                                    {
                                        FirstUnit.Lvl = 2;
                                        FirstUnit.Point += 3;
                                    }
                                    if (FirstUnit.Exp >= 800 && FirstUnit.Exp < 1800)
                                    {
                                        FirstUnit.Lvl = 3;
                                        FirstUnit.Point += 2;
                                    }
                                    if (FirstUnit.Exp >= 800 && FirstUnit.Exp < 1800)
                                    {
                                        FirstUnit.Lvl = 4;
                                        FirstUnit.Point += 2;
                                    }
                                    if (FirstUnit.Exp >= 1800 && FirstUnit.Exp < 3000)
                                    {
                                        FirstUnit.Lvl = 5;
                                        FirstUnit.Point += 5;
                                    }
                                    foreach (var unit in FighterService.FighterList.ToList())
                                    {
                                        if (unit.Name == FirstUnit.Name)
                                        {
                                            FighterService.FighterList.Remove(unit);
                                            FighterService.FighterList.Add(FirstUnit);
                                        }

                                        if (unit.Name == SecondUnit.Name)
                                        {
                                            FighterService.FighterList.Remove(unit);
                                            FighterService.FighterList.Add(SecondUnit);
                                        }
                                    }
                                }

                                else if (SecondUnit.HP - FirstUnit.PhAttack < 0 && pb_SecondPlayerHP.Value - FirstUnit.PhAttack < 0)
                                {
                                    FirstExp += SecondUnit.HP;
                                    SecondUnit.HP = 0;
                                    pb_SecondPlayerHP.Value = 0;
                                    lb_SecondPlayer_HPValue.Content = $"0/{pb_SecondPlayerHP.Maximum}";
                                    MessageBox.Show($"Победил {FirstUnit.Name}!");
                                    FirstUnit.Exp += FirstExp * 3 * SecondUnit.Lvl;
                                    SecondUnit.Exp += SecondExp;
                                    if (FirstUnit.Exp >= 100 && FirstUnit.Exp < 300)
                                    {
                                        FirstUnit.Lvl = 1;
                                        FirstUnit.Point += 2;
                                    }
                                    if (FirstUnit.Exp >= 300 && FirstUnit.Exp < 800)
                                    {
                                        FirstUnit.Lvl = 2;
                                        FirstUnit.Point += 3;
                                    }
                                    if (FirstUnit.Exp >= 800 && FirstUnit.Exp < 1800)
                                    {
                                        FirstUnit.Lvl = 3;
                                        FirstUnit.Point += 2;
                                    }
                                    if (FirstUnit.Exp >= 800 && FirstUnit.Exp < 1800)
                                    {
                                        FirstUnit.Lvl = 4;
                                        FirstUnit.Point += 2;
                                    }
                                    if (FirstUnit.Exp >= 1800 && FirstUnit.Exp < 3000)
                                    {
                                        FirstUnit.Lvl = 5;
                                        FirstUnit.Point += 5;
                                    }
                                    if (SecondUnit.Exp >= 100 && SecondUnit.Exp < 300)
                                    {
                                        SecondUnit.Lvl = 1;
                                        SecondUnit.Point += 2;
                                    }
                                    if (SecondUnit.Exp >= 300 && SecondUnit.Exp < 800)
                                    {
                                        SecondUnit.Lvl = 2;
                                        SecondUnit.Point += 3;
                                    }
                                    if (SecondUnit.Exp >= 800 && SecondUnit.Exp < 1800)
                                    {
                                        SecondUnit.Lvl = 3;
                                        SecondUnit.Point += 2;
                                    }
                                    if (SecondUnit.Exp >= 800 && SecondUnit.Exp < 1800)
                                    {
                                        SecondUnit.Lvl = 4;
                                        SecondUnit.Point += 2;
                                    }
                                    if (SecondUnit.Exp >= 1800 && SecondUnit.Exp < 3000)
                                    {
                                        SecondUnit.Lvl = 5;
                                        SecondUnit.Point += 5;
                                    }
                                    foreach (var unit in FighterService.FighterList.ToList())
                                    {
                                        if (unit.Name == FirstUnit.Name)
                                        {
                                            FighterService.FighterList.Remove(unit);
                                            FighterService.FighterList.Add(FirstUnit);
                                        }

                                        if (unit.Name == SecondUnit.Name)
                                        {
                                            FighterService.FighterList.Remove(unit);
                                            FighterService.FighterList.Add(SecondUnit);
                                        }
                                    }
                                    MainWindow win = new MainWindow();
                                    win.Show();
                                    this.Close();
                                }

                                else
                                {
                                    SecondUnit.HP -= FirstUnit.PhAttack * 2;
                                    pb_SecondPlayerHP.Value = SecondUnit.HP;
                                    lb_SecondPlayer_HPValue.Content = $"{SecondUnit.HP}/{pb_SecondPlayerHP.Maximum}";
                                    Step_ListBox.Items.Add($"{FirstUnit.Name} наносит {SecondUnit.Name} критический урон!");
                                    FirstExp += FirstUnit.PhAttack * 2;
                                }
                            }

                            else
                            {
                                if (FirstUnit.HP == 0)
                                {
                                    MessageBox.Show($"Победил {SecondUnit.Name}!");
                                    SecondUnit.Exp += SecondExp * 3 * FirstUnit.Lvl;
                                    FirstUnit.Exp += FirstExp;
                                    if (SecondUnit.Exp >= 100 && SecondUnit.Exp < 300)
                                    {
                                        SecondUnit.Lvl = 1;
                                        SecondUnit.Point += 2;
                                    }
                                    if (SecondUnit.Exp >= 300 && SecondUnit.Exp < 800)
                                    {
                                        SecondUnit.Lvl = 2;
                                        SecondUnit.Point += 3;
                                    }
                                    if (SecondUnit.Exp >= 800 && SecondUnit.Exp < 1800)
                                    {
                                        SecondUnit.Lvl = 3;
                                        SecondUnit.Point += 2;
                                    }
                                    if (SecondUnit.Exp >= 800 && SecondUnit.Exp < 1800)
                                    {
                                        SecondUnit.Lvl = 4;
                                        SecondUnit.Point += 2;
                                    }
                                    if (SecondUnit.Exp >= 1800 && SecondUnit.Exp < 3000)
                                    {
                                        SecondUnit.Lvl = 5;
                                        SecondUnit.Point += 5;
                                    }
                                    if (FirstUnit.Exp >= 100 && FirstUnit.Exp < 300)
                                    {
                                        FirstUnit.Lvl = 1;
                                        FirstUnit.Point += 2;
                                    }
                                    if (FirstUnit.Exp >= 300 && FirstUnit.Exp < 800)
                                    {
                                        FirstUnit.Lvl = 2;
                                        FirstUnit.Point += 3;
                                    }
                                    if (FirstUnit.Exp >= 800 && FirstUnit.Exp < 1800)
                                    {
                                        FirstUnit.Lvl = 3;
                                        FirstUnit.Point += 2;
                                    }
                                    if (FirstUnit.Exp >= 800 && FirstUnit.Exp < 1800)
                                    {
                                        FirstUnit.Lvl = 4;
                                        FirstUnit.Point += 2;
                                    }
                                    if (FirstUnit.Exp >= 1800 && FirstUnit.Exp < 3000)
                                    {
                                        FirstUnit.Lvl = 5;
                                        FirstUnit.Point += 5;
                                    }
                                    foreach (var unit in FighterService.FighterList.ToList())
                                    {
                                        if (unit.Name == FirstUnit.Name)
                                        {
                                            FighterService.FighterList.Remove(unit);
                                            FighterService.FighterList.Add(FirstUnit);
                                        }

                                        if (unit.Name == SecondUnit.Name)
                                        {
                                            FighterService.FighterList.Remove(unit);
                                            FighterService.FighterList.Add(SecondUnit);
                                        }
                                    }
                                }

                                else if (SecondUnit.HP - FirstUnit.PhAttack < 0 && pb_SecondPlayerHP.Value - FirstUnit.PhAttack < 0)
                                {
                                    FirstExp += SecondUnit.HP;
                                    SecondUnit.HP = 0;
                                    pb_SecondPlayerHP.Value = 0;
                                    lb_SecondPlayer_HPValue.Content = $"0/{pb_SecondPlayerHP.Maximum}";
                                    MessageBox.Show($"Победил {FirstUnit.Name}!");
                                    FirstUnit.Exp += FirstExp * 3 * SecondUnit.Lvl;
                                    SecondUnit.Exp += SecondExp;
                                    if (FirstUnit.Exp >= 100 && FirstUnit.Exp < 300)
                                    {
                                        FirstUnit.Lvl = 1;
                                        FirstUnit.Point += 2;
                                    }
                                    if (FirstUnit.Exp >= 300 && FirstUnit.Exp < 800)
                                    {
                                        FirstUnit.Lvl = 2;
                                        FirstUnit.Point += 3;
                                    }
                                    if (FirstUnit.Exp >= 800 && FirstUnit.Exp < 1800)
                                    {
                                        FirstUnit.Lvl = 3;
                                        FirstUnit.Point += 2;
                                    }
                                    if (FirstUnit.Exp >= 800 && FirstUnit.Exp < 1800)
                                    {
                                        FirstUnit.Lvl = 4;
                                        FirstUnit.Point += 2;
                                    }
                                    if (FirstUnit.Exp >= 1800 && FirstUnit.Exp < 3000)
                                    {
                                        FirstUnit.Lvl = 5;
                                        FirstUnit.Point += 5;
                                    }
                                    if (SecondUnit.Exp >= 100 && SecondUnit.Exp < 300)
                                    {
                                        SecondUnit.Lvl = 1;
                                        SecondUnit.Point += 2;
                                    }
                                    if (SecondUnit.Exp >= 300 && SecondUnit.Exp < 800)
                                    {
                                        SecondUnit.Lvl = 2;
                                        SecondUnit.Point += 3;
                                    }
                                    if (SecondUnit.Exp >= 800 && SecondUnit.Exp < 1800)
                                    {
                                        SecondUnit.Lvl = 3;
                                        SecondUnit.Point += 2;
                                    }
                                    if (SecondUnit.Exp >= 800 && SecondUnit.Exp < 1800)
                                    {
                                        SecondUnit.Lvl = 4;
                                        SecondUnit.Point += 2;
                                    }
                                    if (SecondUnit.Exp >= 1800 && SecondUnit.Exp < 3000)
                                    {
                                        SecondUnit.Lvl = 5;
                                        SecondUnit.Point += 5;
                                    }
                                    foreach (var unit in FighterService.FighterList.ToList())
                                    {
                                        if (unit.Name == FirstUnit.Name)
                                        {
                                            FighterService.FighterList.Remove(unit);
                                            FighterService.FighterList.Add(FirstUnit);
                                        }

                                        if (unit.Name == SecondUnit.Name)
                                        {
                                            FighterService.FighterList.Remove(unit);
                                            FighterService.FighterList.Add(SecondUnit);
                                        }
                                    }
                                    MainWindow win = new MainWindow();
                                    win.Show();
                                    this.Close();
                                }

                                else
                                {
                                    SecondUnit.HP -= FirstUnit.PhAttack;
                                    pb_SecondPlayerHP.Value = SecondUnit.HP;
                                    lb_SecondPlayer_HPValue.Content = $"{SecondUnit.HP}/{pb_SecondPlayerHP.Maximum}";
                                    Step_ListBox.Items.Add($"{FirstUnit.Name} наносит {SecondUnit.Name} обычный урон!");
                                    FirstExp += FirstUnit.PhAttack;
                                }
                            }
                        }

                        else
                        {
                            Step_ListBox.Items.Add($"{SecondUnit.Name} увернулся!");
                        }
                    }

                    else
                    {
                        if (rnd.Next(1, 101) < FirstUnit.BlockBreak)
                        {
                            if (FirstUnit.HP == 0)
                            {
                                MessageBox.Show($"Победил {SecondUnit.Name}!");
                                SecondUnit.Exp += SecondExp * 3 * FirstUnit.Lvl;
                                FirstUnit.Exp += FirstExp;
                                if (SecondUnit.Exp >= 100 && SecondUnit.Exp < 300)
                                {
                                    SecondUnit.Lvl = 1;
                                    SecondUnit.Point += 2;
                                }
                                if (SecondUnit.Exp >= 300 && SecondUnit.Exp < 800)
                                {
                                    SecondUnit.Lvl = 2;
                                    SecondUnit.Point += 3;
                                }
                                if (SecondUnit.Exp >= 800 && SecondUnit.Exp < 1800)
                                {
                                    SecondUnit.Lvl = 3;
                                    SecondUnit.Point += 2;
                                }
                                if (SecondUnit.Exp >= 800 && SecondUnit.Exp < 1800)
                                {
                                    SecondUnit.Lvl = 4;
                                    SecondUnit.Point += 2;
                                }
                                if (SecondUnit.Exp >= 1800 && SecondUnit.Exp < 3000)
                                {
                                    SecondUnit.Lvl = 5;
                                    SecondUnit.Point += 5;
                                }
                                if (FirstUnit.Exp >= 100 && FirstUnit.Exp < 300)
                                {
                                    FirstUnit.Lvl = 1;
                                    FirstUnit.Point += 2;
                                }
                                if (FirstUnit.Exp >= 300 && FirstUnit.Exp < 800)
                                {
                                    FirstUnit.Lvl = 2;
                                    FirstUnit.Point += 3;
                                }
                                if (FirstUnit.Exp >= 800 && FirstUnit.Exp < 1800)
                                {
                                    FirstUnit.Lvl = 3;
                                    FirstUnit.Point += 2;
                                }
                                if (FirstUnit.Exp >= 800 && FirstUnit.Exp < 1800)
                                {
                                    FirstUnit.Lvl = 4;
                                    FirstUnit.Point += 2;
                                }
                                if (FirstUnit.Exp >= 1800 && FirstUnit.Exp < 3000)
                                {
                                    FirstUnit.Lvl = 5;
                                    FirstUnit.Point += 5;
                                }
                                foreach (var unit in FighterService.FighterList.ToList())
                                {
                                    if (unit.Name == FirstUnit.Name)
                                    {
                                        FighterService.FighterList.Remove(unit);
                                        FighterService.FighterList.Add(FirstUnit);
                                    }

                                    if (unit.Name == SecondUnit.Name)
                                    {
                                        FighterService.FighterList.Remove(unit);
                                        FighterService.FighterList.Add(SecondUnit);
                                    }
                                }
                            }

                            else if (SecondUnit.HP - FirstUnit.PhAttack < 0 && pb_SecondPlayerHP.Value - FirstUnit.PhAttack < 0)
                            {
                                FirstExp += SecondUnit.HP;
                                SecondUnit.HP = 0;
                                pb_SecondPlayerHP.Value = 0;
                                lb_SecondPlayer_HPValue.Content = $"0/{pb_SecondPlayerHP.Maximum}";
                                MessageBox.Show($"Победил {FirstUnit.Name}!");
                                FirstUnit.Exp += FirstExp * 3 * SecondUnit.Lvl;
                                SecondUnit.Exp += SecondExp;
                                if (FirstUnit.Exp >= 100 && FirstUnit.Exp < 300)
                                {
                                    FirstUnit.Lvl = 1;
                                    FirstUnit.Point += 2;
                                }
                                if (FirstUnit.Exp >= 300 && FirstUnit.Exp < 800)
                                {
                                    FirstUnit.Lvl = 2;
                                    FirstUnit.Point += 3;
                                }
                                if (FirstUnit.Exp >= 800 && FirstUnit.Exp < 1800)
                                {
                                    FirstUnit.Lvl = 3;
                                    FirstUnit.Point += 2;
                                }
                                if (FirstUnit.Exp >= 800 && FirstUnit.Exp < 1800)
                                {
                                    FirstUnit.Lvl = 4;
                                    FirstUnit.Point += 2;
                                }
                                if (FirstUnit.Exp >= 1800 && FirstUnit.Exp < 3000)
                                {
                                    FirstUnit.Lvl = 5;
                                    FirstUnit.Point += 5;
                                }
                                if (SecondUnit.Exp >= 100 && SecondUnit.Exp < 300)
                                {
                                    SecondUnit.Lvl = 1;
                                    SecondUnit.Point += 2;
                                }
                                if (SecondUnit.Exp >= 300 && SecondUnit.Exp < 800)
                                {
                                    SecondUnit.Lvl = 2;
                                    SecondUnit.Point += 3;
                                }
                                if (SecondUnit.Exp >= 800 && SecondUnit.Exp < 1800)
                                {
                                    SecondUnit.Lvl = 3;
                                    SecondUnit.Point += 2;
                                }
                                if (SecondUnit.Exp >= 800 && SecondUnit.Exp < 1800)
                                {
                                    SecondUnit.Lvl = 4;
                                    SecondUnit.Point += 2;
                                }
                                if (SecondUnit.Exp >= 1800 && SecondUnit.Exp < 3000)
                                {
                                    SecondUnit.Lvl = 5;
                                    SecondUnit.Point += 5;
                                }
                                foreach (var unit in FighterService.FighterList.ToList())
                                {
                                    if (unit.Name == FirstUnit.Name)
                                    {
                                        FighterService.FighterList.Remove(unit);
                                        FighterService.FighterList.Add(FirstUnit);
                                    }

                                    if (unit.Name == SecondUnit.Name)
                                    {
                                        FighterService.FighterList.Remove(unit);
                                        FighterService.FighterList.Add(SecondUnit);
                                    }
                                }
                                MainWindow win = new MainWindow();
                                win.Show();
                                this.Close();
                            }

                            else
                            {
                                SecondUnit.HP -= FirstUnit.PhAttack;
                                pb_SecondPlayerHP.Value = SecondUnit.HP;
                                lb_SecondPlayer_HPValue.Content = $"{SecondUnit.HP}/{pb_SecondPlayerHP.Maximum}";
                                Step_ListBox.Items.Add($"{FirstUnit.Name} наносит {SecondUnit.Name} обычный урон сквозь блок!");
                                FirstExp += FirstUnit.PhAttack;
                            }
                        }

                        else
                        {
                            Step_ListBox.Items.Add($"{FirstUnit.Name} не пробивает блок {SecondUnit.Name}!");
                        }
                    }
                    FirstStep = 0;
                    SecondStep = 1;
                    lb_Step.Content = $"Ход игрока {SecondUnit.Name}!";
                }

                else
                {
                    MessageBox.Show("Заполните все поля!");
                }
            }

            else
            {
                if (cb_FirstPlayerAttack.SelectedItem != null && cb_FirstPlayerBlock.SelectedItem != null && cb_SecondPlayerAttack.SelectedItem != null && cb_SecondPlayerBlock.SelectedItem != null)
                {
                    if (cb_SecondPlayerAttack.SelectedItem.ToString() != cb_FirstPlayerBlock.SelectedItem.ToString())
                    {
                        if (rnd.Next(1, 101) > FirstUnit.Evasion)
                        {
                            if (rnd.Next(1, 101) < SecondUnit.CrtChance)
                            {
                                if (SecondUnit.HP == 0)
                                {
                                    MessageBox.Show($"Победил {FirstUnit.Name}!");
                                    FirstUnit.Exp += FirstExp * 3 * SecondUnit.Lvl;
                                    SecondUnit.Exp += SecondExp;
                                    if (FirstUnit.Exp >= 100 && FirstUnit.Exp < 300)
                                    {
                                        FirstUnit.Lvl = 1;
                                        FirstUnit.Point += 2;
                                    }
                                    if (FirstUnit.Exp >= 300 && FirstUnit.Exp < 800)
                                    {
                                        FirstUnit.Lvl = 2;
                                        FirstUnit.Point += 3;
                                    }
                                    if (FirstUnit.Exp >= 800 && FirstUnit.Exp < 1800)
                                    {
                                        FirstUnit.Lvl = 3;
                                        FirstUnit.Point += 2;
                                    }
                                    if (FirstUnit.Exp >= 800 && FirstUnit.Exp < 1800)
                                    {
                                        FirstUnit.Lvl = 4;
                                        FirstUnit.Point += 2;
                                    }
                                    if (FirstUnit.Exp >= 1800 && FirstUnit.Exp < 3000)
                                    {
                                        FirstUnit.Lvl = 5;
                                        FirstUnit.Point += 5;
                                    }
                                    if (SecondUnit.Exp >= 100 && SecondUnit.Exp < 300)
                                    {
                                        SecondUnit.Lvl = 1;
                                        SecondUnit.Point += 2;
                                    }
                                    if (SecondUnit.Exp >= 300 && SecondUnit.Exp < 800)
                                    {
                                        SecondUnit.Lvl = 2;
                                        SecondUnit.Point += 3;
                                    }
                                    if (SecondUnit.Exp >= 800 && SecondUnit.Exp < 1800)
                                    {
                                        SecondUnit.Lvl = 3;
                                        SecondUnit.Point += 2;
                                    }
                                    if (SecondUnit.Exp >= 800 && SecondUnit.Exp < 1800)
                                    {
                                        SecondUnit.Lvl = 4;
                                        SecondUnit.Point += 2;
                                    }
                                    if (SecondUnit.Exp >= 1800 && SecondUnit.Exp < 3000)
                                    {
                                        SecondUnit.Lvl = 5;
                                        SecondUnit.Point += 5;
                                    }
                                    foreach (var unit in FighterService.FighterList.ToList())
                                    {
                                        if (unit.Name == FirstUnit.Name)
                                        {
                                            FighterService.FighterList.Remove(unit);
                                            FighterService.FighterList.Add(FirstUnit);
                                        }

                                        if (unit.Name == SecondUnit.Name)
                                        {
                                            FighterService.FighterList.Remove(unit);
                                            FighterService.FighterList.Add(SecondUnit);
                                        }
                                    }
                                }

                                else if (FirstUnit.HP - SecondUnit.PhAttack < 0 && pb_FirstPlayerHP.Value - SecondUnit.PhAttack < 0)
                                {
                                    SecondExp += FirstUnit.HP;
                                    FirstUnit.HP = 0;
                                    pb_FirstPlayerHP.Value = 0;
                                    lb_FirstPlayer_HPValue.Content = $"0/{pb_FirstPlayerHP.Maximum}";
                                    MessageBox.Show($"Победил {SecondUnit.Name}!");
                                    SecondUnit.Exp += SecondExp * 3 * FirstUnit.Lvl;
                                    FirstUnit.Exp += FirstExp;
                                    if (SecondUnit.Exp >= 100 && SecondUnit.Exp < 300)
                                    {
                                        SecondUnit.Lvl = 1;
                                        SecondUnit.Point += 2;
                                    }
                                    if (SecondUnit.Exp >= 300 && SecondUnit.Exp < 800)
                                    {
                                        SecondUnit.Lvl = 2;
                                        SecondUnit.Point += 3;
                                    }
                                    if (SecondUnit.Exp >= 800 && SecondUnit.Exp < 1800)
                                    {
                                        SecondUnit.Lvl = 3;
                                        SecondUnit.Point += 2;
                                    }
                                    if (SecondUnit.Exp >= 800 && SecondUnit.Exp < 1800)
                                    {
                                        SecondUnit.Lvl = 4;
                                        SecondUnit.Point += 2;
                                    }
                                    if (SecondUnit.Exp >= 1800 && SecondUnit.Exp < 3000)
                                    {
                                        SecondUnit.Lvl = 5;
                                        SecondUnit.Point += 5;
                                    }
                                    if (FirstUnit.Exp >= 100 && FirstUnit.Exp < 300)
                                    {
                                        FirstUnit.Lvl = 1;
                                        FirstUnit.Point += 2;
                                    }
                                    if (FirstUnit.Exp >= 300 && FirstUnit.Exp < 800)
                                    {
                                        FirstUnit.Lvl = 2;
                                        FirstUnit.Point += 3;
                                    }
                                    if (FirstUnit.Exp >= 800 && FirstUnit.Exp < 1800)
                                    {
                                        FirstUnit.Lvl = 3;
                                        FirstUnit.Point += 2;
                                    }
                                    if (FirstUnit.Exp >= 800 && FirstUnit.Exp < 1800)
                                    {
                                        FirstUnit.Lvl = 4;
                                        FirstUnit.Point += 2;
                                    }
                                    if (FirstUnit.Exp >= 1800 && FirstUnit.Exp < 3000)
                                    {
                                        FirstUnit.Lvl = 5;
                                        FirstUnit.Point += 5;
                                    }
                                    foreach (var unit in FighterService.FighterList.ToList())
                                    {
                                        if (unit.Name == FirstUnit.Name)
                                        {
                                            FighterService.FighterList.Remove(unit);
                                            FighterService.FighterList.Add(FirstUnit);
                                        }

                                        if (unit.Name == SecondUnit.Name)
                                        {
                                            FighterService.FighterList.Remove(unit);
                                            FighterService.FighterList.Add(SecondUnit);
                                        }
                                    }
                                    MainWindow win = new MainWindow();
                                    win.Show();
                                    this.Close();
                                }

                                else
                                {
                                    FirstUnit.HP -= SecondUnit.PhAttack * 2;
                                    pb_FirstPlayerHP.Value = FirstUnit.HP;
                                    lb_FirstPlayer_HPValue.Content = $"{FirstUnit.HP}/{pb_FirstPlayerHP.Maximum}";
                                    Step_ListBox.Items.Add($"{SecondUnit.Name} наносит {FirstUnit.Name} критический урон!");
                                    SecondExp += SecondUnit.PhAttack * 2;
                                }
                            }

                            else
                            {
                                if (SecondUnit.HP == 0)
                                {
                                    MessageBox.Show($"Победил {FirstUnit.Name}!");
                                    FirstUnit.Exp += FirstExp * 3 * SecondUnit.Lvl;
                                    SecondUnit.Exp += SecondExp;
                                    if (FirstUnit.Exp >= 100 && FirstUnit.Exp < 300)
                                    {
                                        FirstUnit.Lvl = 1;
                                        FirstUnit.Point += 2;
                                    }
                                    if (FirstUnit.Exp >= 300 && FirstUnit.Exp < 800)
                                    {
                                        FirstUnit.Lvl = 2;
                                        FirstUnit.Point += 3;
                                    }
                                    if (FirstUnit.Exp >= 800 && FirstUnit.Exp < 1800)
                                    {
                                        FirstUnit.Lvl = 3;
                                        FirstUnit.Point += 2;
                                    }
                                    if (FirstUnit.Exp >= 800 && FirstUnit.Exp < 1800)
                                    {
                                        FirstUnit.Lvl = 4;
                                        FirstUnit.Point += 2;
                                    }
                                    if (FirstUnit.Exp >= 1800 && FirstUnit.Exp < 3000)
                                    {
                                        FirstUnit.Lvl = 5;
                                        FirstUnit.Point += 5;
                                    }
                                    if (SecondUnit.Exp >= 100 && SecondUnit.Exp < 300)
                                    {
                                        SecondUnit.Lvl = 1;
                                        SecondUnit.Point += 2;
                                    }
                                    if (SecondUnit.Exp >= 300 && SecondUnit.Exp < 800)
                                    {
                                        SecondUnit.Lvl = 2;
                                        SecondUnit.Point += 3;
                                    }
                                    if (SecondUnit.Exp >= 800 && SecondUnit.Exp < 1800)
                                    {
                                        SecondUnit.Lvl = 3;
                                        SecondUnit.Point += 2;
                                    }
                                    if (SecondUnit.Exp >= 800 && SecondUnit.Exp < 1800)
                                    {
                                        SecondUnit.Lvl = 4;
                                        SecondUnit.Point += 2;
                                    }
                                    if (SecondUnit.Exp >= 1800 && SecondUnit.Exp < 3000)
                                    {
                                        SecondUnit.Lvl = 5;
                                        SecondUnit.Point += 5;
                                    }
                                    foreach (var unit in FighterService.FighterList.ToList())
                                    {
                                        if (unit.Name == FirstUnit.Name)
                                        {
                                            FighterService.FighterList.Remove(unit);
                                            FighterService.FighterList.Add(FirstUnit);
                                        }

                                        if (unit.Name == SecondUnit.Name)
                                        {
                                            FighterService.FighterList.Remove(unit);
                                            FighterService.FighterList.Add(SecondUnit);
                                        }
                                    }
                                }

                                else if (FirstUnit.HP - SecondUnit.PhAttack < 0 && pb_FirstPlayerHP.Value - SecondUnit.PhAttack < 0)
                                {
                                    SecondExp += FirstUnit.HP;
                                    FirstUnit.HP = 0;
                                    pb_FirstPlayerHP.Value = 0;
                                    lb_FirstPlayer_HPValue.Content = $"0/{pb_FirstPlayerHP.Maximum}";
                                    MessageBox.Show($"Победил {SecondUnit.Name}!");
                                    SecondUnit.Exp += SecondExp * 3 * FirstUnit.Lvl;
                                    FirstUnit.Exp += FirstExp;
                                    if (SecondUnit.Exp >= 100 && SecondUnit.Exp < 300)
                                    {
                                        SecondUnit.Lvl = 1;
                                        SecondUnit.Point += 2;
                                    }
                                    if (SecondUnit.Exp >= 300 && SecondUnit.Exp < 800)
                                    {
                                        SecondUnit.Lvl = 2;
                                        SecondUnit.Point += 3;
                                    }
                                    if (SecondUnit.Exp >= 800 && SecondUnit.Exp < 1800)
                                    {
                                        SecondUnit.Lvl = 3;
                                        SecondUnit.Point += 2;
                                    }
                                    if (SecondUnit.Exp >= 800 && SecondUnit.Exp < 1800)
                                    {
                                        SecondUnit.Lvl = 4;
                                        SecondUnit.Point += 2;
                                    }
                                    if (SecondUnit.Exp >= 1800 && SecondUnit.Exp < 3000)
                                    {
                                        SecondUnit.Lvl = 5;
                                        SecondUnit.Point += 5;
                                    }
                                    if (FirstUnit.Exp >= 100 && FirstUnit.Exp < 300)
                                    {
                                        FirstUnit.Lvl = 1;
                                        FirstUnit.Point += 2;
                                    }
                                    if (FirstUnit.Exp >= 300 && FirstUnit.Exp < 800)
                                    {
                                        FirstUnit.Lvl = 2;
                                        FirstUnit.Point += 3;
                                    }
                                    if (FirstUnit.Exp >= 800 && FirstUnit.Exp < 1800)
                                    {
                                        FirstUnit.Lvl = 3;
                                        FirstUnit.Point += 2;
                                    }
                                    if (FirstUnit.Exp >= 800 && FirstUnit.Exp < 1800)
                                    {
                                        FirstUnit.Lvl = 4;
                                        FirstUnit.Point += 2;
                                    }
                                    if (FirstUnit.Exp >= 1800 && FirstUnit.Exp < 3000)
                                    {
                                        FirstUnit.Lvl = 5;
                                        FirstUnit.Point += 5;
                                    }
                                    foreach (var unit in FighterService.FighterList.ToList())
                                    {
                                        if (unit.Name == FirstUnit.Name)
                                        {
                                            FighterService.FighterList.Remove(unit);
                                            FighterService.FighterList.Add(FirstUnit);
                                        }

                                        if (unit.Name == SecondUnit.Name)
                                        {
                                            FighterService.FighterList.Remove(unit);
                                            FighterService.FighterList.Add(SecondUnit);
                                        }
                                    }
                                    MainWindow win = new MainWindow();
                                    win.Show();
                                    this.Close();
                                }

                                else
                                {
                                    FirstUnit.HP -= SecondUnit.PhAttack;
                                    pb_FirstPlayerHP.Value = FirstUnit.HP;
                                    lb_FirstPlayer_HPValue.Content = $"{FirstUnit.HP}/{pb_FirstPlayerHP.Maximum}";
                                    SecondExp += SecondUnit.PhAttack;
                                    Step_ListBox.Items.Add($"{SecondUnit.Name} наносит {FirstUnit.Name} обычный урон!");
                                }
                            }
                        }

                        else
                        {
                            Step_ListBox.Items.Add($"{FirstUnit.Name} увернулся!");
                        }
                    }

                    else
                    {
                        if (rnd.Next(1, 101) < SecondUnit.BlockBreak)
                        {
                            if (SecondUnit.HP == 0)
                            {
                                MessageBox.Show($"Победил {FirstUnit.Name}!");
                                FirstUnit.Exp += FirstExp * 3 * SecondUnit.Lvl;
                                SecondUnit.Exp += SecondExp;
                                if (FirstUnit.Exp >= 100 && FirstUnit.Exp < 300)
                                {
                                    FirstUnit.Lvl = 1;
                                    FirstUnit.Point += 2;
                                }
                                if (FirstUnit.Exp >= 300 && FirstUnit.Exp < 800)
                                {
                                    FirstUnit.Lvl = 2;
                                    FirstUnit.Point += 3;
                                }
                                if (FirstUnit.Exp >= 800 && FirstUnit.Exp < 1800)
                                {
                                    FirstUnit.Lvl = 3;
                                    FirstUnit.Point += 2;
                                }
                                if (FirstUnit.Exp >= 800 && FirstUnit.Exp < 1800)
                                {
                                    FirstUnit.Lvl = 4;
                                    FirstUnit.Point += 2;
                                }
                                if (FirstUnit.Exp >= 1800 && FirstUnit.Exp < 3000)
                                {
                                    FirstUnit.Lvl = 5;
                                    FirstUnit.Point += 5;
                                }
                                if (SecondUnit.Exp >= 100 && SecondUnit.Exp < 300)
                                {
                                    SecondUnit.Lvl = 1;
                                    SecondUnit.Point += 2;
                                }
                                if (SecondUnit.Exp >= 300 && SecondUnit.Exp < 800)
                                {
                                    SecondUnit.Lvl = 2;
                                    SecondUnit.Point += 3;
                                }
                                if (SecondUnit.Exp >= 800 && SecondUnit.Exp < 1800)
                                {
                                    SecondUnit.Lvl = 3;
                                    SecondUnit.Point += 2;
                                }
                                if (SecondUnit.Exp >= 800 && SecondUnit.Exp < 1800)
                                {
                                    SecondUnit.Lvl = 4;
                                    SecondUnit.Point += 2;
                                }
                                if (SecondUnit.Exp >= 1800 && SecondUnit.Exp < 3000)
                                {
                                    SecondUnit.Lvl = 5;
                                    SecondUnit.Point += 5;
                                }
                                foreach (var unit in FighterService.FighterList.ToList())
                                {
                                    if (unit.Name == FirstUnit.Name)
                                    {
                                        FighterService.FighterList.Remove(unit);
                                        FighterService.FighterList.Add(FirstUnit);
                                    }

                                    if (unit.Name == SecondUnit.Name)
                                    {
                                        FighterService.FighterList.Remove(unit);
                                        FighterService.FighterList.Add(SecondUnit);
                                    }
                                }
                            }

                            else if (FirstUnit.HP - SecondUnit.PhAttack < 0 && pb_FirstPlayerHP.Value - SecondUnit.PhAttack < 0)
                            {
                                SecondExp += FirstUnit.HP;
                                FirstUnit.HP = 0;
                                pb_FirstPlayerHP.Value = 0;
                                lb_FirstPlayer_HPValue.Content = $"0/{pb_FirstPlayerHP.Maximum}";
                                MessageBox.Show($"Победил {SecondUnit.Name}!");
                                SecondUnit.Exp += SecondExp * 3 * FirstUnit.Lvl;
                                FirstUnit.Exp += FirstExp;
                                if (SecondUnit.Exp >= 100 && SecondUnit.Exp < 300)
                                {
                                    SecondUnit.Lvl = 1;
                                    SecondUnit.Point += 2;
                                }
                                if (SecondUnit.Exp >= 300 && SecondUnit.Exp < 800)
                                {
                                    SecondUnit.Lvl = 2;
                                    SecondUnit.Point += 3;
                                }
                                if (SecondUnit.Exp >= 800 && SecondUnit.Exp < 1800)
                                {
                                    SecondUnit.Lvl = 3;
                                    SecondUnit.Point += 2;
                                }
                                if (SecondUnit.Exp >= 800 && SecondUnit.Exp < 1800)
                                {
                                    SecondUnit.Lvl = 4;
                                    SecondUnit.Point += 2;
                                }
                                if (SecondUnit.Exp >= 1800 && SecondUnit.Exp < 3000)
                                {
                                    SecondUnit.Lvl = 5;
                                    SecondUnit.Point += 5;
                                }
                                if (FirstUnit.Exp >= 100 && FirstUnit.Exp < 300)
                                {
                                    FirstUnit.Lvl = 1;
                                    FirstUnit.Point += 2;
                                }
                                if (FirstUnit.Exp >= 300 && FirstUnit.Exp < 800)
                                {
                                    FirstUnit.Lvl = 2;
                                    FirstUnit.Point += 3;
                                }
                                if (FirstUnit.Exp >= 800 && FirstUnit.Exp < 1800)
                                {
                                    FirstUnit.Lvl = 3;
                                    FirstUnit.Point += 2;
                                }
                                if (FirstUnit.Exp >= 800 && FirstUnit.Exp < 1800)
                                {
                                    FirstUnit.Lvl = 4;
                                    FirstUnit.Point += 2;
                                }
                                if (FirstUnit.Exp >= 1800 && FirstUnit.Exp < 3000)
                                {
                                    FirstUnit.Lvl = 5;
                                    FirstUnit.Point += 5;
                                }
                                foreach (var unit in FighterService.FighterList.ToList())
                                {
                                    if (unit.Name == FirstUnit.Name)
                                    {
                                        FighterService.FighterList.Remove(unit);
                                        FighterService.FighterList.Add(FirstUnit);
                                    }

                                    if (unit.Name == SecondUnit.Name)
                                    {
                                        FighterService.FighterList.Remove(unit);
                                        FighterService.FighterList.Add(SecondUnit);
                                    }
                                }
                                MainWindow win = new MainWindow();
                                win.Show();
                                this.Close();
                            }

                            else
                            {
                                FirstUnit.HP -= SecondUnit.PhAttack;
                                pb_FirstPlayerHP.Value = FirstUnit.HP;
                                lb_FirstPlayer_HPValue.Content = $"{FirstUnit.HP}/{pb_FirstPlayerHP.Maximum}";
                                Step_ListBox.Items.Add($"{SecondUnit.Name} наносит {FirstUnit.Name} обычный урон сквозь блок!");
                                SecondExp += SecondUnit.PhAttack;
                            }
                        }

                        else
                        {
                            Step_ListBox.Items.Add($"{SecondUnit.Name} не пробивает блок {FirstUnit.Name}!");
                        }
                    }
                    FirstStep = 1;
                    SecondStep = 0;
                    lb_Step.Content = $"Ход игрока {FirstUnit.Name}!";
                }

                else
                {
                    MessageBox.Show("Заполните все поля!");
                }
            }
        }

        private void MageAtt_button_Click(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            if (FirstStep == 1)
            {
                if (cb_FirstPlayerAttack.SelectedItem != null && cb_FirstPlayerBlock.SelectedItem != null && cb_SecondPlayerAttack.SelectedItem != null && cb_SecondPlayerBlock.SelectedItem != null)
                {
                    if (FirstUnit.Mana >= 5)
                    {
                        if (cb_FirstPlayerAttack.SelectedItem.ToString() != cb_SecondPlayerBlock.SelectedItem.ToString())
                        {
                            if (rnd.Next(1, 101) > SecondUnit.Evasion)
                            {
                                if (rnd.Next(1, 101) < FirstUnit.CrtChance)
                                {
                                    if (FirstUnit.HP == 0)
                                    {
                                        MessageBox.Show($"Победил {SecondUnit.Name}!");
                                        SecondUnit.Exp += SecondExp * 3 * FirstUnit.Lvl;
                                        FirstUnit.Exp += FirstExp;
                                        if (SecondUnit.Exp >= 100 && SecondUnit.Exp < 300)
                                        {
                                            SecondUnit.Lvl = 1;
                                            SecondUnit.Point += 2;
                                        }
                                        if (SecondUnit.Exp >= 300 && SecondUnit.Exp < 800)
                                        {
                                            SecondUnit.Lvl = 2;
                                            SecondUnit.Point += 3;
                                        }
                                        if (SecondUnit.Exp >= 800 && SecondUnit.Exp < 1800)
                                        {
                                            SecondUnit.Lvl = 3;
                                            SecondUnit.Point += 2;
                                        }
                                        if (SecondUnit.Exp >= 800 && SecondUnit.Exp < 1800)
                                        {
                                            SecondUnit.Lvl = 4;
                                            SecondUnit.Point += 2;
                                        }
                                        if (SecondUnit.Exp >= 1800 && SecondUnit.Exp < 3000)
                                        {
                                            SecondUnit.Lvl = 5;
                                            SecondUnit.Point += 5;
                                        }
                                        if (FirstUnit.Exp >= 100 && FirstUnit.Exp < 300)
                                        {
                                            FirstUnit.Lvl = 1;
                                            FirstUnit.Point += 2;
                                        }
                                        if (FirstUnit.Exp >= 300 && FirstUnit.Exp < 800)
                                        {
                                            FirstUnit.Lvl = 2;
                                            FirstUnit.Point += 3;
                                        }
                                        if (FirstUnit.Exp >= 800 && FirstUnit.Exp < 1800)
                                        {
                                            FirstUnit.Lvl = 3;
                                            FirstUnit.Point += 2;
                                        }
                                        if (FirstUnit.Exp >= 800 && FirstUnit.Exp < 1800)
                                        {
                                            FirstUnit.Lvl = 4;
                                            FirstUnit.Point += 2;
                                        }
                                        if (FirstUnit.Exp >= 1800 && FirstUnit.Exp < 3000)
                                        {
                                            FirstUnit.Lvl = 5;
                                            FirstUnit.Point += 5;
                                        }
                                        foreach (var unit in FighterService.FighterList.ToList())
                                        {
                                            if (unit.Name == FirstUnit.Name)
                                            {
                                                FighterService.FighterList.Remove(unit);
                                                FighterService.FighterList.Add(FirstUnit);
                                            }

                                            if (unit.Name == SecondUnit.Name)
                                            {
                                                FighterService.FighterList.Remove(unit);
                                                FighterService.FighterList.Add(SecondUnit);
                                            }
                                        }
                                    }
                                    else if (SecondUnit.HP - FirstUnit.MageAttack < 0 && pb_SecondPlayerHP.Value - FirstUnit.MageAttack < 0)
                                    {
                                        FirstExp += SecondUnit.HP;
                                        SecondUnit.HP = 0;
                                        pb_SecondPlayerHP.Value = 0;
                                        lb_SecondPlayer_HPValue.Content = $"0/{pb_SecondPlayerHP.Maximum}";
                                        MessageBox.Show($"Победил {FirstUnit.Name}!");
                                        FirstUnit.Exp += FirstExp * 3 * SecondUnit.Lvl;
                                        SecondUnit.Exp += SecondExp;
                                        if (FirstUnit.Exp >= 100 && FirstUnit.Exp < 300)
                                        {
                                            FirstUnit.Lvl = 1;
                                            FirstUnit.Point += 2;
                                        }
                                        if (FirstUnit.Exp >= 300 && FirstUnit.Exp < 800)
                                        {
                                            FirstUnit.Lvl = 2;
                                            FirstUnit.Point += 3;
                                        }
                                        if (FirstUnit.Exp >= 800 && FirstUnit.Exp < 1800)
                                        {
                                            FirstUnit.Lvl = 3;
                                            FirstUnit.Point += 2;
                                        }
                                        if (FirstUnit.Exp >= 800 && FirstUnit.Exp < 1800)
                                        {
                                            FirstUnit.Lvl = 4;
                                            FirstUnit.Point += 2;
                                        }
                                        if (FirstUnit.Exp >= 1800 && FirstUnit.Exp < 3000)
                                        {
                                            FirstUnit.Lvl = 5;
                                            FirstUnit.Point += 5;
                                        }
                                        if (SecondUnit.Exp >= 100 && SecondUnit.Exp < 300)
                                        {
                                            SecondUnit.Lvl = 1;
                                            SecondUnit.Point += 2;
                                        }
                                        if (SecondUnit.Exp >= 300 && SecondUnit.Exp < 800)
                                        {
                                            SecondUnit.Lvl = 2;
                                            SecondUnit.Point += 3;
                                        }
                                        if (SecondUnit.Exp >= 800 && SecondUnit.Exp < 1800)
                                        {
                                            SecondUnit.Lvl = 3;
                                            SecondUnit.Point += 2;
                                        }
                                        if (SecondUnit.Exp >= 800 && SecondUnit.Exp < 1800)
                                        {
                                            SecondUnit.Lvl = 4;
                                            SecondUnit.Point += 2;
                                        }
                                        if (SecondUnit.Exp >= 1800 && SecondUnit.Exp < 3000)
                                        {
                                            SecondUnit.Lvl = 5;
                                            SecondUnit.Point += 5;
                                        }
                                        foreach (var unit in FighterService.FighterList.ToList())
                                        {
                                            if (unit.Name == FirstUnit.Name)
                                            {
                                                FighterService.FighterList.Remove(unit);
                                                FighterService.FighterList.Add(FirstUnit);
                                            }

                                            if (unit.Name == SecondUnit.Name)
                                            {
                                                FighterService.FighterList.Remove(unit);
                                                FighterService.FighterList.Add(SecondUnit);
                                            }
                                        }
                                        MainWindow win = new MainWindow();
                                        win.Show();
                                        this.Close();
                                    }

                                    else
                                    {
                                        SecondUnit.HP -= FirstUnit.MageAttack * 2;
                                        pb_SecondPlayerHP.Value = SecondUnit.HP;
                                        lb_SecondPlayer_HPValue.Content = $"{SecondUnit.HP}/{pb_SecondPlayerHP.Maximum}";
                                        Step_ListBox.Items.Add($"{FirstUnit.Name} наносит {SecondUnit.Name} критический магический урон!");
                                        FirstExp += FirstUnit.MageAttack * 2;
                                    }
                                }

                                else
                                {
                                    if (FirstUnit.HP == 0)
                                    {
                                        MessageBox.Show($"Победил {SecondUnit.Name}!");
                                        SecondUnit.Exp += SecondExp * 3 * FirstUnit.Lvl;
                                        FirstUnit.Exp += FirstExp;
                                        if (SecondUnit.Exp >= 100 && SecondUnit.Exp < 300)
                                        {
                                            SecondUnit.Lvl = 1;
                                            SecondUnit.Point += 2;
                                        }
                                        if (SecondUnit.Exp >= 300 && SecondUnit.Exp < 800)
                                        {
                                            SecondUnit.Lvl = 2;
                                            SecondUnit.Point += 3;
                                        }
                                        if (SecondUnit.Exp >= 800 && SecondUnit.Exp < 1800)
                                        {
                                            SecondUnit.Lvl = 3;
                                            SecondUnit.Point += 2;
                                        }
                                        if (SecondUnit.Exp >= 800 && SecondUnit.Exp < 1800)
                                        {
                                            SecondUnit.Lvl = 4;
                                            SecondUnit.Point += 2;
                                        }
                                        if (SecondUnit.Exp >= 1800 && SecondUnit.Exp < 3000)
                                        {
                                            SecondUnit.Lvl = 5;
                                            SecondUnit.Point += 5;
                                        }
                                        if (FirstUnit.Exp >= 100 && FirstUnit.Exp < 300)
                                        {
                                            FirstUnit.Lvl = 1;
                                            FirstUnit.Point += 2;
                                        }
                                        if (FirstUnit.Exp >= 300 && FirstUnit.Exp < 800)
                                        {
                                            FirstUnit.Lvl = 2;
                                            FirstUnit.Point += 3;
                                        }
                                        if (FirstUnit.Exp >= 800 && FirstUnit.Exp < 1800)
                                        {
                                            FirstUnit.Lvl = 3;
                                            FirstUnit.Point += 2;
                                        }
                                        if (FirstUnit.Exp >= 800 && FirstUnit.Exp < 1800)
                                        {
                                            FirstUnit.Lvl = 4;
                                            FirstUnit.Point += 2;
                                        }
                                        if (FirstUnit.Exp >= 1800 && FirstUnit.Exp < 3000)
                                        {
                                            FirstUnit.Lvl = 5;
                                            FirstUnit.Point += 5;
                                        }
                                        foreach (var unit in FighterService.FighterList.ToList())
                                        {
                                            if (unit.Name == FirstUnit.Name)
                                            {
                                                FighterService.FighterList.Remove(unit);
                                                FighterService.FighterList.Add(FirstUnit);
                                            }

                                            if (unit.Name == SecondUnit.Name)
                                            {
                                                FighterService.FighterList.Remove(unit);
                                                FighterService.FighterList.Add(SecondUnit);
                                            }
                                        }
                                    }

                                    else if (SecondUnit.HP - FirstUnit.MageAttack < 0 && pb_SecondPlayerHP.Value - FirstUnit.MageAttack < 0)
                                    {
                                        FirstExp += SecondUnit.HP;
                                        SecondUnit.HP = 0;
                                        pb_SecondPlayerHP.Value = 0;
                                        lb_SecondPlayer_HPValue.Content = $"0/{pb_SecondPlayerHP.Maximum}";
                                        MessageBox.Show($"Победил {FirstUnit.Name}!");
                                        FirstUnit.Exp += FirstExp * 3 * SecondUnit.Lvl;
                                        SecondUnit.Exp += SecondExp;
                                        if (FirstUnit.Exp >= 100 && FirstUnit.Exp < 300)
                                        {
                                            FirstUnit.Lvl = 1;
                                            FirstUnit.Point += 2;
                                        }
                                        if (FirstUnit.Exp >= 300 && FirstUnit.Exp < 800)
                                        {
                                            FirstUnit.Lvl = 2;
                                            FirstUnit.Point += 3;
                                        }
                                        if (FirstUnit.Exp >= 800 && FirstUnit.Exp < 1800)
                                        {
                                            FirstUnit.Lvl = 3;
                                            FirstUnit.Point += 2;
                                        }
                                        if (FirstUnit.Exp >= 800 && FirstUnit.Exp < 1800)
                                        {
                                            FirstUnit.Lvl = 4;
                                            FirstUnit.Point += 2;
                                        }
                                        if (FirstUnit.Exp >= 1800 && FirstUnit.Exp < 3000)
                                        {
                                            FirstUnit.Lvl = 5;
                                            FirstUnit.Point += 5;
                                        }
                                        if (SecondUnit.Exp >= 100 && SecondUnit.Exp < 300)
                                        {
                                            SecondUnit.Lvl = 1;
                                            SecondUnit.Point += 2;
                                        }
                                        if (SecondUnit.Exp >= 300 && SecondUnit.Exp < 800)
                                        {
                                            SecondUnit.Lvl = 2;
                                            SecondUnit.Point += 3;
                                        }
                                        if (SecondUnit.Exp >= 800 && SecondUnit.Exp < 1800)
                                        {
                                            SecondUnit.Lvl = 3;
                                            SecondUnit.Point += 2;
                                        }
                                        if (SecondUnit.Exp >= 800 && SecondUnit.Exp < 1800)
                                        {
                                            SecondUnit.Lvl = 4;
                                            SecondUnit.Point += 2;
                                        }
                                        if (SecondUnit.Exp >= 1800 && SecondUnit.Exp < 3000)
                                        {
                                            SecondUnit.Lvl = 5;
                                            SecondUnit.Point += 5;
                                        }
                                        foreach (var unit in FighterService.FighterList.ToList())
                                        {
                                            if (unit.Name == FirstUnit.Name)
                                            {
                                                FighterService.FighterList.Remove(unit);
                                                FighterService.FighterList.Add(FirstUnit);
                                            }

                                            if (unit.Name == SecondUnit.Name)
                                            {
                                                FighterService.FighterList.Remove(unit);
                                                FighterService.FighterList.Add(SecondUnit);
                                            }
                                        }
                                        MainWindow win = new MainWindow();
                                        win.Show();
                                        this.Close();
                                    }

                                    else
                                    {
                                        SecondUnit.HP -= FirstUnit.MageAttack;
                                        pb_SecondPlayerHP.Value = SecondUnit.HP;
                                        lb_SecondPlayer_HPValue.Content = $"{SecondUnit.HP}/{pb_SecondPlayerHP.Maximum}";
                                        Step_ListBox.Items.Add($"{FirstUnit.Name} наносит {SecondUnit.Name} обычный магический урон!");
                                        FirstExp += FirstUnit.MageAttack;
                                    }
                                }
                            }

                            else
                            {
                                Step_ListBox.Items.Add($"{SecondUnit.Name} увернулся!");
                            }
                        }

                        else
                        {
                            if (rnd.Next(1, 101) < FirstUnit.BlockBreak)
                            {
                                if (FirstUnit.HP == 0)
                                {
                                    MessageBox.Show($"Победил {SecondUnit.Name}!");
                                    SecondUnit.Exp += SecondExp * 3 * FirstUnit.Lvl;
                                    FirstUnit.Exp += FirstExp;
                                    if (SecondUnit.Exp >= 100 && SecondUnit.Exp < 300)
                                    {
                                        SecondUnit.Lvl = 1;
                                        SecondUnit.Point += 2;
                                    }
                                    if (SecondUnit.Exp >= 300 && SecondUnit.Exp < 800)
                                    {
                                        SecondUnit.Lvl = 2;
                                        SecondUnit.Point += 3;
                                    }
                                    if (SecondUnit.Exp >= 800 && SecondUnit.Exp < 1800)
                                    {
                                        SecondUnit.Lvl = 3;
                                        SecondUnit.Point += 2;
                                    }
                                    if (SecondUnit.Exp >= 800 && SecondUnit.Exp < 1800)
                                    {
                                        SecondUnit.Lvl = 4;
                                        SecondUnit.Point += 2;
                                    }
                                    if (SecondUnit.Exp >= 1800 && SecondUnit.Exp < 3000)
                                    {
                                        SecondUnit.Lvl = 5;
                                        SecondUnit.Point += 5;
                                    }
                                    if (FirstUnit.Exp >= 100 && FirstUnit.Exp < 300)
                                    {
                                        FirstUnit.Lvl = 1;
                                        FirstUnit.Point += 2;
                                    }
                                    if (FirstUnit.Exp >= 300 && FirstUnit.Exp < 800)
                                    {
                                        FirstUnit.Lvl = 2;
                                        FirstUnit.Point += 3;
                                    }
                                    if (FirstUnit.Exp >= 800 && FirstUnit.Exp < 1800)
                                    {
                                        FirstUnit.Lvl = 3;
                                        FirstUnit.Point += 2;
                                    }
                                    if (FirstUnit.Exp >= 800 && FirstUnit.Exp < 1800)
                                    {
                                        FirstUnit.Lvl = 4;
                                        FirstUnit.Point += 2;
                                    }
                                    if (FirstUnit.Exp >= 1800 && FirstUnit.Exp < 3000)
                                    {
                                        FirstUnit.Lvl = 5;
                                        FirstUnit.Point += 5;
                                    }
                                    foreach (var unit in FighterService.FighterList.ToList())
                                    {
                                        if (unit.Name == FirstUnit.Name)
                                        {
                                            FighterService.FighterList.Remove(unit);
                                            FighterService.FighterList.Add(FirstUnit);
                                        }

                                        if (unit.Name == SecondUnit.Name)
                                        {
                                            FighterService.FighterList.Remove(unit);
                                            FighterService.FighterList.Add(SecondUnit);
                                        }
                                    }
                                }

                                else if (SecondUnit.HP - FirstUnit.MageAttack < 0 && pb_SecondPlayerHP.Value - FirstUnit.MageAttack < 0)
                                {
                                    FirstExp += SecondUnit.HP;
                                    SecondUnit.HP = 0;
                                    pb_SecondPlayerHP.Value = 0;
                                    lb_SecondPlayer_HPValue.Content = $"0/{pb_SecondPlayerHP.Maximum}";
                                    MessageBox.Show($"Победил {FirstUnit.Name}!");
                                    FirstUnit.Exp += FirstExp * 3 * SecondUnit.Lvl;
                                    SecondUnit.Exp += SecondExp;
                                    if (FirstUnit.Exp >= 100 && FirstUnit.Exp < 300)
                                    {
                                        FirstUnit.Lvl = 1;
                                        FirstUnit.Point += 2;
                                    }
                                    if (FirstUnit.Exp >= 300 && FirstUnit.Exp < 800)
                                    {
                                        FirstUnit.Lvl = 2;
                                        FirstUnit.Point += 3;
                                    }
                                    if (FirstUnit.Exp >= 800 && FirstUnit.Exp < 1800)
                                    {
                                        FirstUnit.Lvl = 3;
                                        FirstUnit.Point += 2;
                                    }
                                    if (FirstUnit.Exp >= 800 && FirstUnit.Exp < 1800)
                                    {
                                        FirstUnit.Lvl = 4;
                                        FirstUnit.Point += 2;
                                    }
                                    if (FirstUnit.Exp >= 1800 && FirstUnit.Exp < 3000)
                                    {
                                        FirstUnit.Lvl = 5;
                                        FirstUnit.Point += 5;
                                    }
                                    if (SecondUnit.Exp >= 100 && SecondUnit.Exp < 300)
                                    {
                                        SecondUnit.Lvl = 1;
                                        SecondUnit.Point += 2;
                                    }
                                    if (SecondUnit.Exp >= 300 && SecondUnit.Exp < 800)
                                    {
                                        SecondUnit.Lvl = 2;
                                        SecondUnit.Point += 3;
                                    }
                                    if (SecondUnit.Exp >= 800 && SecondUnit.Exp < 1800)
                                    {
                                        SecondUnit.Lvl = 3;
                                        SecondUnit.Point += 2;
                                    }
                                    if (SecondUnit.Exp >= 800 && SecondUnit.Exp < 1800)
                                    {
                                        SecondUnit.Lvl = 4;
                                        SecondUnit.Point += 2;
                                    }
                                    if (SecondUnit.Exp >= 1800 && SecondUnit.Exp < 3000)
                                    {
                                        SecondUnit.Lvl = 5;
                                        SecondUnit.Point += 5;
                                    }
                                    foreach (var unit in FighterService.FighterList.ToList())
                                    {
                                        if (unit.Name == FirstUnit.Name)
                                        {
                                            FighterService.FighterList.Remove(unit);
                                            FighterService.FighterList.Add(FirstUnit);
                                        }

                                        if (unit.Name == SecondUnit.Name)
                                        {
                                            FighterService.FighterList.Remove(unit);
                                            FighterService.FighterList.Add(SecondUnit);
                                        }
                                    }
                                    MainWindow win = new MainWindow();
                                    win.Show();
                                    this.Close();
                                }

                                else
                                {
                                    SecondUnit.HP -= FirstUnit.MageAttack;
                                    pb_SecondPlayerHP.Value = SecondUnit.HP;
                                    lb_SecondPlayer_HPValue.Content = $"{SecondUnit.HP}/{pb_SecondPlayerHP.Maximum}";
                                    Step_ListBox.Items.Add($"{FirstUnit.Name} наносит {SecondUnit.Name} обычный магический урон сквозь блок!");
                                    FirstExp += FirstUnit.MageAttack;
                                }
                            }

                            else
                            {
                                Step_ListBox.Items.Add($"{FirstUnit.Name} не пробивает блок {SecondUnit.Name}!");
                            }
                        }
                        FirstUnit.Mana -= 5;
                        pb_FirstPlayerMana.Value = FirstUnit.Mana;
                        lb_FirstPlayer_ManaValue.Content = $"{FirstUnit.Mana}/{pb_FirstPlayerMana.Maximum}";
                        FirstStep = 0;
                        SecondStep = 1;
                        lb_Step.Content = $"Ход игрока {SecondUnit.Name}!";
                    }

                    else
                    {
                        MessageBox.Show("Не хватает маны!");
                    }
                }

                else
                {
                    MessageBox.Show("Заполните все поля!");
                }
            }

            else
            {
                if (cb_FirstPlayerAttack.SelectedItem != null && cb_FirstPlayerBlock.SelectedItem != null && cb_SecondPlayerAttack.SelectedItem != null && cb_SecondPlayerBlock.SelectedItem != null)
                {
                    if (SecondUnit.Mana >= 5)
                    {
                        if (cb_SecondPlayerAttack.SelectedItem.ToString() != cb_FirstPlayerBlock.SelectedItem.ToString())
                        {
                            if (rnd.Next(1, 101) > FirstUnit.Evasion)
                            {
                                if (rnd.Next(1, 101) < SecondUnit.CrtChance)
                                {
                                    if (SecondUnit.HP == 0)
                                    {
                                        MessageBox.Show($"Победил {FirstUnit.Name}!");
                                        FirstUnit.Exp += FirstExp * 3 * SecondUnit.Lvl;
                                        SecondUnit.Exp += SecondExp;
                                        if (FirstUnit.Exp >= 100 && FirstUnit.Exp < 300)
                                        {
                                            FirstUnit.Lvl = 1;
                                            FirstUnit.Point += 2;
                                        }
                                        if (FirstUnit.Exp >= 300 && FirstUnit.Exp < 800)
                                        {
                                            FirstUnit.Lvl = 2;
                                            FirstUnit.Point += 3;
                                        }
                                        if (FirstUnit.Exp >= 800 && FirstUnit.Exp < 1800)
                                        {
                                            FirstUnit.Lvl = 3;
                                            FirstUnit.Point += 2;
                                        }
                                        if (FirstUnit.Exp >= 800 && FirstUnit.Exp < 1800)
                                        {
                                            FirstUnit.Lvl = 4;
                                            FirstUnit.Point += 2;
                                        }
                                        if (FirstUnit.Exp >= 1800 && FirstUnit.Exp < 3000)
                                        {
                                            FirstUnit.Lvl = 5;
                                            FirstUnit.Point += 5;
                                        }
                                        if (SecondUnit.Exp >= 100 && SecondUnit.Exp < 300)
                                        {
                                            SecondUnit.Lvl = 1;
                                            SecondUnit.Point += 2;
                                        }
                                        if (SecondUnit.Exp >= 300 && SecondUnit.Exp < 800)
                                        {
                                            SecondUnit.Lvl = 2;
                                            SecondUnit.Point += 3;
                                        }
                                        if (SecondUnit.Exp >= 800 && SecondUnit.Exp < 1800)
                                        {
                                            SecondUnit.Lvl = 3;
                                            SecondUnit.Point += 2;
                                        }
                                        if (SecondUnit.Exp >= 800 && SecondUnit.Exp < 1800)
                                        {
                                            SecondUnit.Lvl = 4;
                                            SecondUnit.Point += 2;
                                        }
                                        if (SecondUnit.Exp >= 1800 && SecondUnit.Exp < 3000)
                                        {
                                            SecondUnit.Lvl = 5;
                                            SecondUnit.Point += 5;
                                        }
                                        foreach (var unit in FighterService.FighterList.ToList())
                                        {
                                            if (unit.Name == FirstUnit.Name)
                                            {
                                                FighterService.FighterList.Remove(unit);
                                                FighterService.FighterList.Add(FirstUnit);
                                            }

                                            if (unit.Name == SecondUnit.Name)
                                            {
                                                FighterService.FighterList.Remove(unit);
                                                FighterService.FighterList.Add(SecondUnit);
                                            }
                                        }
                                    }

                                    else if (FirstUnit.HP - SecondUnit.MageAttack < 0 && pb_FirstPlayerHP.Value - SecondUnit.MageAttack < 0)
                                    {
                                        SecondExp += FirstUnit.HP;
                                        FirstUnit.HP = 0;
                                        pb_FirstPlayerHP.Value = 0;
                                        lb_FirstPlayer_HPValue.Content = $"0/{pb_FirstPlayerHP.Maximum}";
                                        MessageBox.Show($"Победил {SecondUnit.Name}!");
                                        SecondUnit.Exp += SecondExp * 3 * FirstUnit.Lvl;
                                        FirstUnit.Exp += FirstExp;
                                        if (SecondUnit.Exp >= 100 && SecondUnit.Exp < 300)
                                        {
                                            SecondUnit.Lvl = 1;
                                            SecondUnit.Point += 2;
                                        }
                                        if (SecondUnit.Exp >= 300 && SecondUnit.Exp < 800)
                                        {
                                            SecondUnit.Lvl = 2;
                                            SecondUnit.Point += 3;
                                        }
                                        if (SecondUnit.Exp >= 800 && SecondUnit.Exp < 1800)
                                        {
                                            SecondUnit.Lvl = 3;
                                            SecondUnit.Point += 2;
                                        }
                                        if (SecondUnit.Exp >= 800 && SecondUnit.Exp < 1800)
                                        {
                                            SecondUnit.Lvl = 4;
                                            SecondUnit.Point += 2;
                                        }
                                        if (SecondUnit.Exp >= 1800 && SecondUnit.Exp < 3000)
                                        {
                                            SecondUnit.Lvl = 5;
                                            SecondUnit.Point += 5;
                                        }
                                        if (FirstUnit.Exp >= 100 && FirstUnit.Exp < 300)
                                        {
                                            FirstUnit.Lvl = 1;
                                            FirstUnit.Point += 2;
                                        }
                                        if (FirstUnit.Exp >= 300 && FirstUnit.Exp < 800)
                                        {
                                            FirstUnit.Lvl = 2;
                                            FirstUnit.Point += 3;
                                        }
                                        if (FirstUnit.Exp >= 800 && FirstUnit.Exp < 1800)
                                        {
                                            FirstUnit.Lvl = 3;
                                            FirstUnit.Point += 2;
                                        }
                                        if (FirstUnit.Exp >= 800 && FirstUnit.Exp < 1800)
                                        {
                                            FirstUnit.Lvl = 4;
                                            FirstUnit.Point += 2;
                                        }
                                        if (FirstUnit.Exp >= 1800 && FirstUnit.Exp < 3000)
                                        {
                                            FirstUnit.Lvl = 5;
                                            FirstUnit.Point += 5;
                                        }
                                        foreach (var unit in FighterService.FighterList.ToList())
                                        {
                                            if (unit.Name == FirstUnit.Name)
                                            {
                                                FighterService.FighterList.Remove(unit);
                                                FighterService.FighterList.Add(FirstUnit);
                                            }

                                            if (unit.Name == SecondUnit.Name)
                                            {
                                                FighterService.FighterList.Remove(unit);
                                                FighterService.FighterList.Add(SecondUnit);
                                            }
                                        }
                                        MainWindow win = new MainWindow();
                                        win.Show();
                                        this.Close();
                                    }

                                    else
                                    {
                                        FirstUnit.HP -= SecondUnit.MageAttack * 2;
                                        pb_FirstPlayerHP.Value = FirstUnit.HP;
                                        lb_FirstPlayer_HPValue.Content = $"{FirstUnit.HP}/{pb_FirstPlayerHP.Maximum}";
                                        Step_ListBox.Items.Add($"{SecondUnit.Name} наносит {FirstUnit.Name} критический магический урон!");
                                        SecondExp += SecondUnit.MageAttack * 2;
                                    }
                                }

                                else
                                {
                                    if (SecondUnit.HP == 0)
                                    {
                                        MessageBox.Show($"Победил {FirstUnit.Name}!");
                                        FirstUnit.Exp += FirstExp * 3 * SecondUnit.Lvl;
                                        SecondUnit.Exp += SecondExp;
                                        if (FirstUnit.Exp >= 100 && FirstUnit.Exp < 300)
                                        {
                                            FirstUnit.Lvl = 1;
                                            FirstUnit.Point += 2;
                                        }
                                        if (FirstUnit.Exp >= 300 && FirstUnit.Exp < 800)
                                        {
                                            FirstUnit.Lvl = 2;
                                            FirstUnit.Point += 3;
                                        }
                                        if (FirstUnit.Exp >= 800 && FirstUnit.Exp < 1800)
                                        {
                                            FirstUnit.Lvl = 3;
                                            FirstUnit.Point += 2;
                                        }
                                        if (FirstUnit.Exp >= 800 && FirstUnit.Exp < 1800)
                                        {
                                            FirstUnit.Lvl = 4;
                                            FirstUnit.Point += 2;
                                        }
                                        if (FirstUnit.Exp >= 1800 && FirstUnit.Exp < 3000)
                                        {
                                            FirstUnit.Lvl = 5;
                                            FirstUnit.Point += 5;
                                        }
                                        if (SecondUnit.Exp >= 100 && SecondUnit.Exp < 300)
                                        {
                                            SecondUnit.Lvl = 1;
                                            SecondUnit.Point += 2;
                                        }
                                        if (SecondUnit.Exp >= 300 && SecondUnit.Exp < 800)
                                        {
                                            SecondUnit.Lvl = 2;
                                            SecondUnit.Point += 3;
                                        }
                                        if (SecondUnit.Exp >= 800 && SecondUnit.Exp < 1800)
                                        {
                                            SecondUnit.Lvl = 3;
                                            SecondUnit.Point += 2;
                                        }
                                        if (SecondUnit.Exp >= 800 && SecondUnit.Exp < 1800)
                                        {
                                            SecondUnit.Lvl = 4;
                                            SecondUnit.Point += 2;
                                        }
                                        if (SecondUnit.Exp >= 1800 && SecondUnit.Exp < 3000)
                                        {
                                            SecondUnit.Lvl = 5;
                                            SecondUnit.Point += 5;
                                        }
                                        foreach (var unit in FighterService.FighterList.ToList())
                                        {
                                            if (unit.Name == FirstUnit.Name)
                                            {
                                                FighterService.FighterList.Remove(unit);
                                                FighterService.FighterList.Add(FirstUnit);
                                            }

                                            if (unit.Name == SecondUnit.Name)
                                            {
                                                FighterService.FighterList.Remove(unit);
                                                FighterService.FighterList.Add(SecondUnit);
                                            }
                                        }
                                    }

                                    else if (FirstUnit.HP - SecondUnit.MageAttack < 0 && pb_FirstPlayerHP.Value - SecondUnit.MageAttack < 0)
                                    {
                                        SecondExp += FirstUnit.HP;
                                        FirstUnit.HP = 0;
                                        pb_FirstPlayerHP.Value = 0;
                                        lb_FirstPlayer_HPValue.Content = $"0/{pb_FirstPlayerHP.Maximum}";
                                        MessageBox.Show($"Победил {SecondUnit.Name}!");
                                        SecondUnit.Exp += SecondExp * 3 * FirstUnit.Lvl;
                                        FirstUnit.Exp += FirstExp;
                                        if (SecondUnit.Exp >= 100 && SecondUnit.Exp < 300)
                                        {
                                            SecondUnit.Lvl = 1;
                                            SecondUnit.Point += 2;
                                        }
                                        if (SecondUnit.Exp >= 300 && SecondUnit.Exp < 800)
                                        {
                                            SecondUnit.Lvl = 2;
                                            SecondUnit.Point += 3;
                                        }
                                        if (SecondUnit.Exp >= 800 && SecondUnit.Exp < 1800)
                                        {
                                            SecondUnit.Lvl = 3;
                                            SecondUnit.Point += 2;
                                        }
                                        if (SecondUnit.Exp >= 800 && SecondUnit.Exp < 1800)
                                        {
                                            SecondUnit.Lvl = 4;
                                            SecondUnit.Point += 2;
                                        }
                                        if (SecondUnit.Exp >= 1800 && SecondUnit.Exp < 3000)
                                        {
                                            SecondUnit.Lvl = 5;
                                            SecondUnit.Point += 5;
                                        }
                                        if (FirstUnit.Exp >= 100 && FirstUnit.Exp < 300)
                                        {
                                            FirstUnit.Lvl = 1;
                                            FirstUnit.Point += 2;
                                        }
                                        if (FirstUnit.Exp >= 300 && FirstUnit.Exp < 800)
                                        {
                                            FirstUnit.Lvl = 2;
                                            FirstUnit.Point += 3;
                                        }
                                        if (FirstUnit.Exp >= 800 && FirstUnit.Exp < 1800)
                                        {
                                            FirstUnit.Lvl = 3;
                                            FirstUnit.Point += 2;
                                        }
                                        if (FirstUnit.Exp >= 800 && FirstUnit.Exp < 1800)
                                        {
                                            FirstUnit.Lvl = 4;
                                            FirstUnit.Point += 2;
                                        }
                                        if (FirstUnit.Exp >= 1800 && FirstUnit.Exp < 3000)
                                        {
                                            FirstUnit.Lvl = 5;
                                            FirstUnit.Point += 5;
                                        }
                                        foreach (var unit in FighterService.FighterList.ToList())
                                        {
                                            if (unit.Name == FirstUnit.Name)
                                            {
                                                FighterService.FighterList.Remove(unit);
                                                FighterService.FighterList.Add(FirstUnit);
                                            }

                                            if (unit.Name == SecondUnit.Name)
                                            {
                                                FighterService.FighterList.Remove(unit);
                                                FighterService.FighterList.Add(SecondUnit);
                                            }
                                        }
                                        MainWindow win = new MainWindow();
                                        win.Show();
                                        this.Close();
                                    }

                                    else
                                    {
                                        FirstUnit.HP -= SecondUnit.MageAttack;
                                        pb_FirstPlayerHP.Value = FirstUnit.HP;
                                        lb_FirstPlayer_HPValue.Content = $"{FirstUnit.HP}/{pb_FirstPlayerHP.Maximum}";
                                        Step_ListBox.Items.Add($"{SecondUnit.Name} наносит {FirstUnit.Name} обычный магический урон!");
                                        SecondExp += SecondUnit.MageAttack;
                                    }
                                }
                            }

                            else
                            {
                                Step_ListBox.Items.Add($"{FirstUnit.Name} увернулся!");
                            }
                        }

                        else
                        {
                            if (rnd.Next(1, 101) < SecondUnit.BlockBreak)
                            {
                                if (SecondUnit.HP == 0)
                                {
                                    MessageBox.Show($"Победил {FirstUnit.Name}!");
                                    FirstUnit.Exp += FirstExp * 3 * SecondUnit.Lvl;
                                    SecondUnit.Exp += SecondExp;
                                    if (FirstUnit.Exp >= 100 && FirstUnit.Exp < 300)
                                    {
                                        FirstUnit.Lvl = 1;
                                        FirstUnit.Point += 2;
                                    }
                                    if (FirstUnit.Exp >= 300 && FirstUnit.Exp < 800)
                                    {
                                        FirstUnit.Lvl = 2;
                                        FirstUnit.Point += 3;
                                    }
                                    if (FirstUnit.Exp >= 800 && FirstUnit.Exp < 1800)
                                    {
                                        FirstUnit.Lvl = 3;
                                        FirstUnit.Point += 2;
                                    }
                                    if (FirstUnit.Exp >= 800 && FirstUnit.Exp < 1800)
                                    {
                                        FirstUnit.Lvl = 4;
                                        FirstUnit.Point += 2;
                                    }
                                    if (FirstUnit.Exp >= 1800 && FirstUnit.Exp < 3000)
                                    {
                                        FirstUnit.Lvl = 5;
                                        FirstUnit.Point += 5;
                                    }
                                    if (SecondUnit.Exp >= 100 && SecondUnit.Exp < 300)
                                    {
                                        SecondUnit.Lvl = 1;
                                        SecondUnit.Point += 2;
                                    }
                                    if (SecondUnit.Exp >= 300 && SecondUnit.Exp < 800)
                                    {
                                        SecondUnit.Lvl = 2;
                                        SecondUnit.Point += 3;
                                    }
                                    if (SecondUnit.Exp >= 800 && SecondUnit.Exp < 1800)
                                    {
                                        SecondUnit.Lvl = 3;
                                        SecondUnit.Point += 2;
                                    }
                                    if (SecondUnit.Exp >= 800 && SecondUnit.Exp < 1800)
                                    {
                                        SecondUnit.Lvl = 4;
                                        SecondUnit.Point += 2;
                                    }
                                    if (SecondUnit.Exp >= 1800 && SecondUnit.Exp < 3000)
                                    {
                                        SecondUnit.Lvl = 5;
                                        SecondUnit.Point += 5;
                                    }
                                    foreach (var unit in FighterService.FighterList.ToList())
                                    {
                                        if (unit.Name == FirstUnit.Name)
                                        {
                                            FighterService.FighterList.Remove(unit);
                                            FighterService.FighterList.Add(FirstUnit);
                                        }

                                        if (unit.Name == SecondUnit.Name)
                                        {
                                            FighterService.FighterList.Remove(unit);
                                            FighterService.FighterList.Add(SecondUnit);
                                        }
                                    }
                                }

                                else if (FirstUnit.HP - SecondUnit.MageAttack < 0 && pb_FirstPlayerHP.Value - SecondUnit.MageAttack < 0)
                                {
                                    SecondExp += FirstUnit.HP;
                                    FirstUnit.HP = 0;
                                    pb_FirstPlayerHP.Value = 0;
                                    lb_FirstPlayer_HPValue.Content = $"0/{pb_FirstPlayerHP.Maximum}";
                                    MessageBox.Show($"Победил {SecondUnit.Name}!");
                                    SecondUnit.Exp += SecondExp * 3 * FirstUnit.Lvl;
                                    FirstUnit.Exp += FirstExp;
                                    if (SecondUnit.Exp >= 100 && SecondUnit.Exp < 300)
                                    {
                                        SecondUnit.Lvl = 1;
                                        SecondUnit.Point += 2;
                                    }
                                    if (SecondUnit.Exp >= 300 && SecondUnit.Exp < 800)
                                    {
                                        SecondUnit.Lvl = 2;
                                        SecondUnit.Point += 3;
                                    }
                                    if (SecondUnit.Exp >= 800 && SecondUnit.Exp < 1800)
                                    {
                                        SecondUnit.Lvl = 3;
                                        SecondUnit.Point += 2;
                                    }
                                    if (SecondUnit.Exp >= 800 && SecondUnit.Exp < 1800)
                                    {
                                        SecondUnit.Lvl = 4;
                                        SecondUnit.Point += 2;
                                    }
                                    if (SecondUnit.Exp >= 1800 && SecondUnit.Exp < 3000)
                                    {
                                        SecondUnit.Lvl = 5;
                                        SecondUnit.Point += 5;
                                    }
                                    if (FirstUnit.Exp >= 100 && FirstUnit.Exp < 300)
                                    {
                                        FirstUnit.Lvl = 1;
                                        FirstUnit.Point += 2;
                                    }
                                    if (FirstUnit.Exp >= 300 && FirstUnit.Exp < 800)
                                    {
                                        FirstUnit.Lvl = 2;
                                        FirstUnit.Point += 3;
                                    }
                                    if (FirstUnit.Exp >= 800 && FirstUnit.Exp < 1800)
                                    {
                                        FirstUnit.Lvl = 3;
                                        FirstUnit.Point += 2;
                                    }
                                    if (FirstUnit.Exp >= 800 && FirstUnit.Exp < 1800)
                                    {
                                        FirstUnit.Lvl = 4;
                                        FirstUnit.Point += 2;
                                    }
                                    if (FirstUnit.Exp >= 1800 && FirstUnit.Exp < 3000)
                                    {
                                        FirstUnit.Lvl = 5;
                                        FirstUnit.Point += 5;
                                    }
                                    foreach (var unit in FighterService.FighterList.ToList())
                                    {
                                        if (unit.Name == FirstUnit.Name)
                                        {
                                            FighterService.FighterList.Remove(unit);
                                            FighterService.FighterList.Add(FirstUnit);
                                        }

                                        if (unit.Name == SecondUnit.Name)
                                        {
                                            FighterService.FighterList.Remove(unit);
                                            FighterService.FighterList.Add(SecondUnit);
                                        }
                                    }
                                    MainWindow win = new MainWindow();
                                    win.Show();
                                    this.Close();
                                }

                                else
                                {
                                    FirstUnit.HP -= SecondUnit.MageAttack;
                                    pb_FirstPlayerHP.Value = FirstUnit.HP;
                                    lb_FirstPlayer_HPValue.Content = $"{FirstUnit.HP}/{pb_FirstPlayerHP.Maximum}";
                                    Step_ListBox.Items.Add($"{SecondUnit.Name} наносит {FirstUnit.Name} обычный магический урон сквозь блок!");
                                    SecondExp += SecondUnit.MageAttack;
                                }
                            }

                            else
                            {
                                Step_ListBox.Items.Add($"{SecondUnit.Name} не пробивает блок {FirstUnit.Name}!");
                            }
                        }
                        SecondUnit.Mana -= 5;
                        pb_SecondPlayerMana.Value = SecondUnit.Mana;
                        lb_SecondPlayer_ManaValue.Content = $"{SecondUnit.Mana}/{pb_SecondPlayerMana.Maximum}";
                        FirstStep = 1;
                        SecondStep = 0;
                        lb_Step.Content = $"Ход игрока {FirstUnit.Name}!";
                    }

                    else
                    {
                        MessageBox.Show("Не хватает маны!");
                    }
                }

                else
                {
                    MessageBox.Show("Заполните все поля!");
                }
            }
        }

        private void Heal_button_Click(object sender, RoutedEventArgs e)
        {
            if (FirstStep == 1)
            {
                if (FirstUnit.Intelligence > 5)
                {
                    if (FirstUnit.Mana >= 2)
                    {
                        if (FirstUnit.HP + 2 > FirstUnit.MaxHP)
                        {
                            FirstUnit.HP = FirstUnit.MaxHP;
                            pb_FirstPlayerHP.Value = FirstUnit.HP;
                            lb_FirstPlayer_HPValue.Content = $"{FirstUnit.HP}/{pb_FirstPlayerHP.Maximum}";
                        }

                        else
                        {
                            FirstUnit.HP += 1;
                            pb_FirstPlayerHP.Value = FirstUnit.HP;
                            lb_FirstPlayer_HPValue.Content = $"{FirstUnit.HP}/{pb_FirstPlayerHP.Maximum}";
                        }
                        FirstUnit.Mana -= 2;
                        pb_FirstPlayerMana.Value = FirstUnit.Mana;
                        lb_FirstPlayer_ManaValue.Content = $"{FirstUnit.Mana}/{pb_FirstPlayerMana.Maximum}";
                        FirstStep = 0;
                        SecondStep = 1;
                        lb_Step.Content = $"Ход игрока {SecondUnit.Name}!";
                    }

                    else
                    {
                        MessageBox.Show("Не хватает маны!");
                    }
                }

                else
                {
                    MessageBox.Show("Не хватает интеллекта!");
                }
            }

            else
            {
                if (SecondUnit.Intelligence > 5)
                {
                    if (SecondUnit.Mana >= 2)
                    {
                        if (SecondUnit.HP + 2 > SecondUnit.MaxHP)
                        {
                            SecondUnit.HP = SecondUnit.MaxHP;
                            pb_SecondPlayerHP.Value = FirstUnit.HP;
                            lb_SecondPlayer_HPValue.Content = $"{SecondUnit.HP}/{pb_SecondPlayerHP.Maximum}";
                        }

                        else
                        {
                            SecondUnit.HP += 1;
                            pb_SecondPlayerHP.Value = FirstUnit.HP;
                            lb_SecondPlayer_HPValue.Content = $"{SecondUnit.HP}/{pb_SecondPlayerHP.Maximum}";
                        }
                        SecondUnit.Mana -= 2;
                        pb_SecondPlayerMana.Value = SecondUnit.Mana;
                        lb_SecondPlayer_ManaValue.Content = $"{SecondUnit.Mana}/{pb_SecondPlayerHP.Maximum}";
                        FirstStep = 1;
                        SecondStep = 0;
                        lb_Step.Content = $"Ход игрока {FirstUnit.Name}!";
                    }

                    else
                    {
                        MessageBox.Show("Не хватает маны!");
                    }
                }

                else
                {
                    MessageBox.Show("Не хватает интеллекта!");
                }
            }
        }
    }
}
