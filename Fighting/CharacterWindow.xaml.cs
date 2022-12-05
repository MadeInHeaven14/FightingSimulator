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
    /// Логика взаимодействия для CharacterWindow.xaml
    /// </summary>
    public partial class CharacterWindow : Window
    {
        int Strength;
        int Dexterity;
        int Luck;
        int Constitution;
        int Intelligence;
        int Point;
        Fighter fighter;
        public CharacterWindow()
        {
            InitializeComponent();
            foreach (var unit in FighterService.FighterList)
            {
                ListViewFighters.Items.Add(unit);
            }
        }

        private void ListViewFighters_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Fighter choosedFighter = (Fighter)ListViewFighters.SelectedItem;
            tb_Name.Text = choosedFighter.Name;
            lb_StrengthValue.Content = choosedFighter.Strength;
            lb_DexterityValue.Content = choosedFighter.Dexterity;
            lb_LuckValue.Content = choosedFighter.Luck;
            lb_ConstitutionValue.Content = choosedFighter.Constitution;
            lb_IntelligenceValue.Content = choosedFighter.Intelligence;
            lb_LvlValue.Content = choosedFighter.Lvl;
            lb_ExpValue.Content = choosedFighter.Exp;
            lb_PointValue.Content = choosedFighter.Point;
            Point = Convert.ToInt32(lb_PointValue.Content);
            Strength = Convert.ToInt32(lb_StrengthValue.Content);
            Dexterity = Convert.ToInt32(lb_DexterityValue.Content);
            Luck = Convert.ToInt32(lb_LuckValue.Content);
            Constitution = Convert.ToInt32(lb_ConstitutionValue.Content);
            Intelligence = Convert.ToInt32(lb_IntelligenceValue.Content);
        }

        private void Plus_Strength_button_Click(object sender, RoutedEventArgs e)
        {
            if (Point > 0)
            {
                Strength++;
                lb_StrengthValue.Content = Strength.ToString();
                Point--;
                lb_PointValue.Content = Point.ToString();
            }

            else
            {
                MessageBox.Show("Не хватает очков улучшения!");
            }
        }

        private void Plus_Dexterity_button_Click(object sender, RoutedEventArgs e)
        {
            if (Point > 0)
            {
                Dexterity++;
                lb_DexterityValue.Content = Dexterity.ToString();
                Point--;
                lb_PointValue.Content = Point.ToString();
            }

            else
            {
                MessageBox.Show("Не хватает очков улучшения!");
            }
        }

        private void Plus_Intelligence_button_Click(object sender, RoutedEventArgs e)
        {
            if (Point > 0)
            {
                Intelligence++;
                lb_IntelligenceValue.Content = Intelligence.ToString();
                Point--;
                lb_PointValue.Content = Point.ToString();
            }

            else
            {
                MessageBox.Show("Не хватает очков улучшения!");
            }
        }

        private void Plus_Luck_button_Click(object sender, RoutedEventArgs e)
        {
            if (Point > 0)
            {
                Luck++;
                lb_LuckValue.Content = Luck.ToString();
                Point--;
                lb_PointValue.Content = Point.ToString();
            }

            else
            {
                MessageBox.Show("Не хватает очков улучшения!");
            }
        }

        private void Plus_Constitution_button_Click(object sender, RoutedEventArgs e)
        {
            if (Point > 0)
            {
                Constitution++;
                lb_ConstitutionValue.Content = Constitution.ToString();
                Point--;
                lb_PointValue.Content = Point.ToString();
            }

            else
            {
                MessageBox.Show("Не хватает очков улучшения!");
            }
        }

        private void Redact_button_Click(object sender, RoutedEventArgs e)
        {
            fighter.Strength = Convert.ToInt32(lb_StrengthValue.Content);
            fighter.Dexterity = Convert.ToInt32(lb_DexterityValue.Content);
            fighter.Luck = Convert.ToInt32(lb_LuckValue.Content);
            fighter.Constitution = Convert.ToInt32(lb_ConstitutionValue.Content);
            fighter.Intelligence = Convert.ToInt32(lb_IntelligenceValue.Content);
            fighter.Lvl = Convert.ToInt32(lb_LvlValue.Content);
            fighter.Exp = Convert.ToInt32(lb_ExpValue.Content);
            fighter.Point = Convert.ToInt32(lb_PointValue.Content);
            foreach (var unit in FighterService.FighterList)
            {
                if (unit.Name == fighter.Name)
                {
                    FighterService.FighterList.Remove(unit);
                    FighterService.FighterList.Add(fighter);
                    ListViewFighters.Items.Remove(unit);
                    ListViewFighters.Items.Add(fighter);
                }
            }
            this.Close();
        }

        private void Back_button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow win = new MainWindow();
            win.Show();
            this.Close();
        }
    }
}
