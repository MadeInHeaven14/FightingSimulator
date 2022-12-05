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
    /// Логика взаимодействия для CharacterCreate.xaml
    /// </summary>
    public partial class CharacterCreate : Window
    {
        int Strength = 1;
        int Dexterity = 1;
        int Luck = 1;
        int Constitution = 1;
        int Intelligence = 1;
        int Points = 15;
        public CharacterCreate()
        {
            InitializeComponent();
        }

        private void Plus_Strength_button_Click(object sender, RoutedEventArgs e)
        {
            if (Points > 0)
            {
                Strength++;
                lb_StrengthValue.Content = Strength.ToString();
                Points--;
                lb_PointsValue.Content = Points.ToString();
            }

            else
            {
                MessageBox.Show("Не хватает очков улучшения!");
            }
        }

        private void Minus_Strength_button_Click(object sender, RoutedEventArgs e)
        {
            if (Strength > 1)
            {
                Strength--;
                lb_StrengthValue.Content = Strength.ToString();
                Points++;
                lb_PointsValue.Content = Points.ToString();
            }

            else
            {
                MessageBox.Show("Больше нельзя уменьшать навык силы!");
            }
        }

        private void Minus_Dexterity_button_Click(object sender, RoutedEventArgs e)
        {
            if (Dexterity > 1)
            {
                Dexterity--;
                lb_DexterityValue.Content = Dexterity.ToString();
                Points++;
                lb_PointsValue.Content = Points.ToString();
            }

            else
            {
                MessageBox.Show("Больше нельзя уменьшать навык ловкости!");
            }
        }

        private void Plus_Dexterity_button_Click(object sender, RoutedEventArgs e)
        {
            if (Points > 0)
            {
                Dexterity++;
                lb_DexterityValue.Content = Dexterity.ToString();
                Points--;
                lb_PointsValue.Content = Points.ToString();
            }

            else
            {
                MessageBox.Show("Не хватает очков улучшения!");
            }
        }

        private void Minus_Luck_button_Click(object sender, RoutedEventArgs e)
        {
            if (Luck > 1)
            {
                Luck--;
                lb_LuckValue.Content = Luck.ToString();
                Points++;
                lb_PointsValue.Content = Points.ToString();
            }

            else
            {
                MessageBox.Show("Больше нельзя уменьшать навык удачи!");
            }
        }

        private void Plus_Luck_button_Click(object sender, RoutedEventArgs e)
        {
            if (Points > 0)
            {
                Luck++;
                lb_LuckValue.Content = Luck.ToString();
                Points--;
                lb_PointsValue.Content = Points.ToString();
            }

            else
            {
                MessageBox.Show("Не хватает очков улучшения!");
            }
        }

        private void Minus_Constitution_button_Click(object sender, RoutedEventArgs e)
        {
            if (Constitution > 1)
            {
                Constitution--;
                lb_ConstitutionValue.Content = Constitution.ToString();
                Points++;
                lb_PointsValue.Content = Points.ToString();
            }

            else
            {
                MessageBox.Show("Больше нельзя уменьшать навык телосложения!");
            }
        }

        private void Plus_Constitution_button_Click(object sender, RoutedEventArgs e)
        {
            if (Points > 0)
            {
                Constitution++;
                lb_ConstitutionValue.Content = Constitution.ToString();
                Points--;
                lb_PointsValue.Content = Points.ToString();
            }

            else
            {
                MessageBox.Show("Не хватает очков улучшения!");
            }
        }

        private void Minus_Intelligence_button_Click(object sender, RoutedEventArgs e)
        {
            if (Intelligence > 1)
            {
                Intelligence--;
                lb_IntelligenceValue.Content = Intelligence.ToString();
                Points++;
                lb_PointsValue.Content = Points.ToString();
            }

            else
            {
                MessageBox.Show("Больше нельзя уменьшать навык интеллекта!");
            }
        }

        private void Plus_Intelligence_button_Click(object sender, RoutedEventArgs e)
        {
            if (Points > 0)
            {
                Intelligence++;
                lb_IntelligenceValue.Content = Intelligence.ToString();
                Points--;
                lb_PointsValue.Content = Points.ToString();
            }

            else
            {
                MessageBox.Show("Не хватает очков улучшения!");
            }
        }

        private void Create_button_Click(object sender, RoutedEventArgs e)
        {
            if (tb_Name.Text != string.Empty)
            {
                Fighter fighter = new Fighter(tb_Name.Text, Strength, Dexterity, Luck, Constitution, Intelligence);
                fighter.Point = Points;
                FighterService.CreateFirstPlayer(fighter);
                this.Close();
            }
            
            else
            {
                MessageBox.Show("Заполните имя персонажа!");
            }
        }
    }
}
