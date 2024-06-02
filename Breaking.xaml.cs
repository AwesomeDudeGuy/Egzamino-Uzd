using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for Breaking.xaml
    /// </summary>
    public partial class Breaking : Window
    {
        public static string pass;
        public static string passmatch;
        private static  bool tread = true;
        public static string time;
        public static string compass;
        public static string computedkeys;
        private static string result;
        private static bool isMatched = false;
        private static int charactersToTestLength = 0;
        private static long computedKeys = 0;
        private static char[] charactersToTest =
{
        'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
        'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
        'u', 'v', 'w', 'x', 'y', 'z','A','B','C','D','E',
        'F','G','H','I','J','K','L','M','N','O','P','Q','R',
        'S','T','U','V','W','X','Y','Z','1','2','3','4','5',
        '6','7','8','9','0'
    };
        private static int smallToTestLength = 0;
        private static char[] smallToTest =
{
        'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
        'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
        'u', 'v', 'w', 'x', 'y', 'z' };
        private static int largeToTestLength = 0;
        private static char[] largeToTest = {'A','B','C','D','E',
        'F','G','H','I','J','K','L','M','N','O','P','Q','R',
        'S','T','U','V','W','X','Y','Z'};
        private static int numToTestLength = 0;
        private static char[] numToTest = {'1','2','3','4','5',
        '6','7','8','9','0'};
        private static int smallnumToTestLength = 0;
        private static char[] smallnumToTest = smallToTest.Concat(numToTest).ToArray();
        private static int largenumToTestLength = 0;
        private static char[] largenumToTest = largeToTest.Concat(numToTest).ToArray();
        private static int largesmallToTestLength = 0;
        private static char[] largesmallToTest = largeToTest.Concat(smallToTest).ToArray();
        public Breaking()
        {
            InitializeComponent();
        }



        public static void Breakopen()
        {

            var timeStarted = DateTime.Now;
            charactersToTestLength = charactersToTest.Length;
            var estimatedPasswordLength = 0;

            while (!isMatched)
            {
                estimatedPasswordLength++;
                var keyLength = estimatedPasswordLength;
                var keyChars = (from c in new char[keyLength] select charactersToTest[0]).ToArray();
                var indexOfLastChar = keyLength - 1;
                CreateNewKey(0, keyChars, keyLength, indexOfLastChar, pass);


            }
            passmatch = "Password Match: " + DateTime.Now.ToString();
            time = "Time Passed: " + DateTime.Now.Subtract(timeStarted).TotalSeconds;
            compass = "Found Password: " + result;
            computedkeys = "Computed Keys: " + computedKeys;
            tread = false;
        }
        private static void CreateNewKey(int currentCharPosition, char[] keyChars, int keyLength, int indexOfLastChar, string pass)
        {
            var nextCharPosition = currentCharPosition + 1;
            for (int i = 0; i < charactersToTestLength; i++)
            {
                keyChars[currentCharPosition] = charactersToTest[i];

                if (currentCharPosition < indexOfLastChar)
                {
                    CreateNewKey(nextCharPosition, keyChars, keyLength, indexOfLastChar, pass);

                    if ((new String(keyChars)) == pass)
                    {
                        if (!isMatched)
                        {
                            isMatched = true;
                            result = new String(keyChars);
                        }
                        return;
                    }
                }

                else
                {
                    computedKeys++;

                    if ((new String(keyChars)) == pass)
                    {
                        if (!isMatched)
                        {
                            isMatched = true;
                            result = new String(keyChars);
                        }
                        return;
                    }
                }
            }
        }
        void ThreadFunk()
        {
            while (tread)
            {
                Breakopen();
                Thread.Sleep(100);
            }
        }


        public static void BreakopenSmall()
        {

            var timeStarted = DateTime.Now;
            smallToTestLength = smallToTest.Length;
            var estimatedPasswordLength = 0;

            while (!isMatched)
            {
                estimatedPasswordLength++;
                var keyLength = estimatedPasswordLength;
                var keyChars = (from c in new char[keyLength] select smallToTest[0]).ToArray();
                var indexOfLastChar = keyLength - 1;
                CreateNewKeySmall(0, keyChars, keyLength, indexOfLastChar, pass);


            }
            passmatch = "Password Match: " + DateTime.Now.ToString();
            time = "Time Passed: " + DateTime.Now.Subtract(timeStarted).TotalSeconds;
            compass = "Found Password: " + result;
            computedkeys = "Computed Keys: " + computedKeys;
        }
        private static void CreateNewKeySmall(int currentCharPosition, char[] keyChars, int keyLength, int indexOfLastChar, string pass)
        {
            var nextCharPosition = currentCharPosition + 1;
            for (int i = 0; i < smallToTestLength; i++)
            {
                keyChars[currentCharPosition] = smallToTest[i];

                if (currentCharPosition < indexOfLastChar)
                {
                    CreateNewKeySmall(nextCharPosition, keyChars, keyLength, indexOfLastChar, pass);

                    if ((new String(keyChars)) == pass)
                    {
                        if (!isMatched)
                        {
                            isMatched = true;
                            result = new String(keyChars);
                        }
                        return;
                    }
                }

                else
                {
                    computedKeys++;

                    if ((new String(keyChars)) == pass)
                    {
                        if (!isMatched)
                        {
                            isMatched = true;
                            result = new String(keyChars);
                        }
                        return;
                    }
                }
            }
        }

        public static void BreakopenLarge()
        {

            var timeStarted = DateTime.Now;
            largeToTestLength = largeToTest.Length;
            var estimatedPasswordLength = 0;

            while (!isMatched)
            {
                estimatedPasswordLength++;
                var keyLength = estimatedPasswordLength;
                var keyChars = (from c in new char[keyLength] select largeToTest[0]).ToArray();
                var indexOfLastChar = keyLength - 1;
                CreateNewKeyLarge(0, keyChars, keyLength, indexOfLastChar, pass);


            }
            passmatch = "Password Match: " + DateTime.Now.ToString();
            time = "Time Passed: " + DateTime.Now.Subtract(timeStarted).TotalSeconds;
            compass = "Found Password: " + result;
            computedkeys = "Computed Keys: " + computedKeys;
        }
        private static void CreateNewKeyLarge(int currentCharPosition, char[] keyChars, int keyLength, int indexOfLastChar, string pass)
        {
            var nextCharPosition = currentCharPosition + 1;
            for (int i = 0; i < largeToTestLength; i++)
            {
                keyChars[currentCharPosition] = largeToTest[i];

                if (currentCharPosition < indexOfLastChar)
                {
                    CreateNewKeyLarge(nextCharPosition, keyChars, keyLength, indexOfLastChar, pass);

                    if ((new String(keyChars)) == pass)
                    {
                        if (!isMatched)
                        {
                            isMatched = true;
                            result = new String(keyChars);
                        }
                        return;
                    }
                }

                else
                {
                    computedKeys++;

                    if ((new String(keyChars)) == pass)
                    {
                        if (!isMatched)
                        {
                            isMatched = true;
                            result = new String(keyChars);
                        }
                        return;
                    }
                }
            }
        }

        public static void BreakopenNum()
        {

            var timeStarted = DateTime.Now;
            numToTestLength = numToTest.Length;
            var estimatedPasswordLength = 0;

            while (!isMatched)
            {
                estimatedPasswordLength++;
                var keyLength = estimatedPasswordLength;
                var keyChars = (from c in new char[keyLength] select numToTest[0]).ToArray();
                var indexOfLastChar = keyLength - 1;
                CreateNewKeyNum(0, keyChars, keyLength, indexOfLastChar, pass);


            }
            passmatch = "Password Match: " + DateTime.Now.ToString();
            time = "Time Passed: " + DateTime.Now.Subtract(timeStarted).TotalSeconds;
            compass = "Found Password: " + result;
            computedkeys = "Computed Keys: " + computedKeys;
        }
        private static void CreateNewKeyNum(int currentCharPosition, char[] keyChars, int keyLength, int indexOfLastChar, string pass)
        {
            var nextCharPosition = currentCharPosition + 1;
            for (int i = 0; i < numToTestLength; i++)
            {
                keyChars[currentCharPosition] = numToTest[i];

                if (currentCharPosition < indexOfLastChar)
                {
                    CreateNewKeyNum(nextCharPosition, keyChars, keyLength, indexOfLastChar, pass);

                    if ((new String(keyChars)) == pass)
                    {
                        if (!isMatched)
                        {
                            isMatched = true;
                            result = new String(keyChars);
                        }
                        return;
                    }
                }

                else
                {
                    computedKeys++;

                    if ((new String(keyChars)) == pass)
                    {
                        if (!isMatched)
                        {
                            isMatched = true;
                            result = new String(keyChars);
                        }
                        return;
                    }
                }
            }
        }

        public static void BreakopenSmallNum()
        {

            var timeStarted = DateTime.Now;
            smallnumToTestLength = smallnumToTest.Length;
            var estimatedPasswordLength = 0;

            while (!isMatched)
            {
                estimatedPasswordLength++;
                var keyLength = estimatedPasswordLength;
                var keyChars = (from c in new char[keyLength] select smallnumToTest[0]).ToArray();
                var indexOfLastChar = keyLength - 1;
                CreateNewKeySmallNum(0, keyChars, keyLength, indexOfLastChar, pass);


            }
            passmatch = "Password Match: " + DateTime.Now.ToString();
            time = "Time Passed: " + DateTime.Now.Subtract(timeStarted).TotalSeconds;
            compass = "Found Password: " + result;
            computedkeys = "Computed Keys: " + computedKeys;
        }
        private static void CreateNewKeySmallNum(int currentCharPosition, char[] keyChars, int keyLength, int indexOfLastChar, string pass)
        {
            var nextCharPosition = currentCharPosition + 1;
            for (int i = 0; i < smallnumToTestLength; i++)
            {
                keyChars[currentCharPosition] = smallnumToTest[i];

                if (currentCharPosition < indexOfLastChar)
                {
                    CreateNewKeySmallNum(nextCharPosition, keyChars, keyLength, indexOfLastChar, pass);

                    if ((new String(keyChars)) == pass)
                    {
                        if (!isMatched)
                        {
                            isMatched = true;
                            result = new String(keyChars);
                        }
                        return;
                    }
                }

                else
                {
                    computedKeys++;

                    if ((new String(keyChars)) == pass)
                    {
                        if (!isMatched)
                        {
                            isMatched = true;
                            result = new String(keyChars);
                        }
                        return;
                    }
                }
            }
        }

        public static void BreakopenLargeNum()
        {

            var timeStarted = DateTime.Now;
            largenumToTestLength = largenumToTest.Length;
            var estimatedPasswordLength = 0;

            while (!isMatched)
            {
                estimatedPasswordLength++;
                var keyLength = estimatedPasswordLength;
                var keyChars = (from c in new char[keyLength] select largenumToTest[0]).ToArray();
                var indexOfLastChar = keyLength - 1;
                CreateNewKeyLargeNum(0, keyChars, keyLength, indexOfLastChar, pass);


            }
            passmatch = "Password Match: " + DateTime.Now.ToString();
            time = "Time Passed: " + DateTime.Now.Subtract(timeStarted).TotalSeconds;
            compass = "Found Password: " + result;
            computedkeys = "Computed Keys: " + computedKeys;
        }
        private static void CreateNewKeyLargeNum(int currentCharPosition, char[] keyChars, int keyLength, int indexOfLastChar, string pass)
        {
            var nextCharPosition = currentCharPosition + 1;
            for (int i = 0; i < largenumToTestLength; i++)
            {
                keyChars[currentCharPosition] = largenumToTest[i];

                if (currentCharPosition < indexOfLastChar)
                {
                    CreateNewKeyLargeNum(nextCharPosition, keyChars, keyLength, indexOfLastChar, pass);

                    if ((new String(keyChars)) == pass)
                    {
                        if (!isMatched)
                        {
                            isMatched = true;
                            result = new String(keyChars);
                        }
                        return;
                    }
                }

                else
                {
                    computedKeys++;

                    if ((new String(keyChars)) == pass)
                    {
                        if (!isMatched)
                        {
                            isMatched = true;
                            result = new String(keyChars);
                        }
                        return;
                    }
                }
            }
        }

        public static void BreakopenLargeSmall()
        {

            var timeStarted = DateTime.Now;
            largesmallToTestLength = largesmallToTest.Length;
            var estimatedPasswordLength = 0;

            while (!isMatched)
            {
                estimatedPasswordLength++;
                var keyLength = estimatedPasswordLength;
                var keyChars = (from c in new char[keyLength] select largesmallToTest[0]).ToArray();
                var indexOfLastChar = keyLength - 1;
                CreateNewKeyLargeSmall(0, keyChars, keyLength, indexOfLastChar, pass);


            }
            passmatch = "Password Match: " + DateTime.Now.ToString();
            time = "Time Passed: " + DateTime.Now.Subtract(timeStarted).TotalSeconds;
            compass = "Found Password: " + result;
            computedkeys = "Computed Keys: " + computedKeys;
        }
        private static void CreateNewKeyLargeSmall(int currentCharPosition, char[] keyChars, int keyLength, int indexOfLastChar, string pass)
        {
            var nextCharPosition = currentCharPosition + 1;
            for (int i = 0; i < largesmallToTestLength; i++)
            {
                keyChars[currentCharPosition] = largesmallToTest[i];

                if (currentCharPosition < indexOfLastChar)
                {
                    CreateNewKeyLargeSmall(nextCharPosition, keyChars, keyLength, indexOfLastChar, pass);

                    if ((new String(keyChars)) == pass)
                    {
                        if (!isMatched)
                        {
                            isMatched = true;
                            result = new String(keyChars);
                        }
                        return;
                    }
                }

                else
                {
                    computedKeys++;

                    if ((new String(keyChars)) == pass)
                    {
                        if (!isMatched)
                        {
                            isMatched = true;
                            result = new String(keyChars);
                        }
                        return;
                    }
                }
            }
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            Small.Visibility = Visibility.Hidden;
            Large.Visibility = Visibility.Hidden;
            Number.Visibility = Visibility.Hidden;
            NumSmall.Visibility = Visibility.Hidden;
            NumLarge.Visibility = Visibility.Hidden;
            SmallLarge.Visibility = Visibility.Hidden;

            Thread thread1 = new Thread(ThreadFunk);
            if (!thread1.IsAlive)
            {
                thread1.Start();
            }

            PassMatch.Content = passmatch;
            Time.Content = time;
            ComPass.Content = compass;
            ComputedKeys.Content = computedkeys;

        }

        private void Small_Click(object sender, RoutedEventArgs e)
        {
            Full.Visibility = Visibility.Hidden;
            Large.Visibility= Visibility.Hidden;
            Number.Visibility = Visibility.Hidden;
            NumSmall.Visibility = Visibility.Hidden;
            NumLarge.Visibility = Visibility.Hidden;
            SmallLarge.Visibility = Visibility.Hidden;

            Thread thread2 = new Thread(BreakopenSmall);

            if (!thread2.IsAlive)
            {
                thread2.Start();
            }
            PassMatch.Content = passmatch;
            Time.Content = time;
            ComPass.Content = compass;
            ComputedKeys.Content = computedkeys;
        }

        private void Large_Click(object sender, RoutedEventArgs e)
        {
            Full.Visibility = Visibility.Hidden;
            Small.Visibility = Visibility.Hidden;
            Number.Visibility = Visibility.Hidden;
            NumSmall.Visibility = Visibility.Hidden;
            NumLarge.Visibility = Visibility.Hidden;
            SmallLarge.Visibility = Visibility.Hidden;

            Thread thread3 = new Thread(BreakopenLarge);
            if (!thread3.IsAlive)
            {
                thread3.Start();
            }
            PassMatch.Content = passmatch;
            Time.Content = time;
            ComPass.Content = compass;
            ComputedKeys.Content = computedkeys;

        }

        private void Number_Click(object sender, RoutedEventArgs e)
        {
            Full.Visibility = Visibility.Hidden;
            Large.Visibility = Visibility.Hidden;
            Small.Visibility = Visibility.Hidden;
            NumSmall.Visibility = Visibility.Hidden;
            NumLarge.Visibility = Visibility.Hidden;
            SmallLarge.Visibility = Visibility.Hidden;

            Thread thread4 = new Thread(BreakopenNum);
            if (!thread4.IsAlive)
            {
                thread4.Start();
            }
            PassMatch.Content = passmatch;
            Time.Content = time;
            ComPass.Content = compass;
            ComputedKeys.Content = computedkeys;
        }

        private void NumSmall_Click(object sender, RoutedEventArgs e)
        {
            Full.Visibility = Visibility.Hidden;
            Large.Visibility = Visibility.Hidden;
            Small.Visibility = Visibility.Hidden;
            Number.Visibility =Visibility.Hidden;
            NumLarge.Visibility = Visibility.Hidden;
            SmallLarge.Visibility = Visibility.Hidden;

            Thread thread5 = new Thread(BreakopenSmallNum);
            if (!thread5.IsAlive)
            {
                thread5.Start();
            }
            PassMatch.Content = passmatch;
            Time.Content = time;
            ComPass.Content = compass;
            ComputedKeys.Content = computedkeys;
        }

        private void NumLarge_Click(object sender, RoutedEventArgs e)
        {
            Full.Visibility = Visibility.Hidden;
            Large.Visibility = Visibility.Hidden;
            Small.Visibility = Visibility.Hidden;
            Number.Visibility = Visibility.Hidden;
            NumSmall.Visibility = Visibility.Hidden;
            SmallLarge.Visibility = Visibility.Hidden;

            Thread thread6 = new Thread(BreakopenLargeNum);
            if (!thread6.IsAlive)
            {
                thread6.Start();
            }
            PassMatch.Content = passmatch;
            Time.Content = time;
            ComPass.Content = compass;
            ComputedKeys.Content = computedkeys;
        }

        private void SmallLarge_Click(object sender, RoutedEventArgs e)
        {
            Full.Visibility = Visibility.Hidden;
            Large.Visibility = Visibility.Hidden;
            Small.Visibility = Visibility.Hidden;
            Number.Visibility = Visibility.Hidden;
            NumSmall.Visibility = Visibility.Hidden;
            NumLarge.Visibility = Visibility.Hidden;

            Thread thread7 = new Thread(BreakopenLargeSmall);
            if (!thread7.IsAlive)
            {
                thread7.Start();
            }
            PassMatch.Content = passmatch;
            Time.Content = time;
            ComPass.Content = compass;
            ComputedKeys.Content = computedkeys;
        }
    }
}
