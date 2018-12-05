using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace lab4_mash
{
    static class WordBulder
    {
        public static byte RGBinSTR(Color RGB, out char a)
        {
            string R = Convert.ToString(RGB.R, 2);
            string G = Convert.ToString(RGB.G, 2);
            string B = Convert.ToString(RGB.B, 2);
            string word = Validate(ref R).Substring(R.Length-2);
            word += Validate(ref G).Substring(G.Length-3);
            word += Validate(ref B).Substring(B.Length-3);
            a = (char)(Convert.ToByte(word, 2));
            return Convert.ToByte(word, 2);
        }

        public static Color ByteToColor(Color In,byte ascii)
        {
            Color NEw = new Color();
            string asc = Convert.ToString(ascii, 2);
            asc = Validate(ref asc);
            string R = Convert.ToString(In.R, 2);
            string G = Convert.ToString(In.G, 2);
            string B = Convert.ToString(In.B, 2);
            R = Validate(ref R);
            R = R.Substring(0, R.Length - 2)+asc.Substring(0, 2);
            G = Validate(ref G);
            G = G.Substring(0, G.Length - 3)+asc.Substring(2, 3);
            B = Validate(ref B);
            B = B.Substring(0, B.Length - 3)+asc.Substring(5, 3);
            NEw = Color.FromArgb(Convert.ToByte(R, 2), Convert.ToByte(G, 2), Convert.ToByte(B, 2));
            return NEw;

        }
        private static string Validate(ref string rgb)
        {
            if(rgb.Length != 8)
            {
                char[] charArray = rgb.ToCharArray();
                Array.Reverse(charArray);
                while (charArray.Length != 8)
                {
                    int size = charArray.Length+1;
                    Array.Resize(ref charArray, size);
                    charArray[size-1] = '0';
                }
                Array.Reverse(charArray);
                rgb = default;
                foreach (char a in charArray)
                    rgb += a;
            }
            return rgb;
        }

    }
}
