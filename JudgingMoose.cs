using System;

/*
             When determining the age of a bull moose, the number of tines (sharp points), extending from the main antlers,
            can be used. An older bull moose tends to have more tines than a younger moose. However, just counting the number
            of tines can be misleading, as a moose can break off the tines, for example when fighting with other moose.
            Therefore, a point system is used when describing the antlers of a bull moose.
            The point system works like this: If the number of tines on the left side and the right side match,
            the moose is said to have the even sum of the number of points. So, “an even 66-point moose”, would have three
            tines on each side. If the moose has a different number of tines on the left and right side, the moose is said
            to have twice the highest number of tines, but it is odd. So “an odd 1010-point moose” would have 55 tines on
            one side, and 44 or less tines on the other side.

            Can you figure out how many points a moose has, given the number of tines on the left and right side?

            Input
            The input contains a single line with two integers \ellℓ and rr, where 0 \le \ell \le 200≤ℓ≤20
            is the number of tines on the left, and 0 \le r \le 200≤r≤20 is the number of tines on the right.

            Output
            Output a single line describing the moose. For even pointed moose, output “Even xx” where xx
            is the points of the moose. For odd pointed moose, output “Odd xx” where xx is the points of
            the moose. If the moose has no tines, output “Not a moose”
 */
namespace KattisProblem
{
    internal class JudgingMoose
    {
        public static void Main()
        {
            string line;
            while ((line = Console.ReadLine()) != null)
            {
                string[] split = line.Split(new char[] { ' ' }, StringSplitOptions.None);
                long left = Int64.Parse(split[0]);
                long right = Int64.Parse(split[1]);


                if (left == 0 && right == 0)
                {
                    Console.WriteLine($"Not a moose");
                    break;
                }

                if (left != right || right != left)
                {
                    long highestOdd = (left > right) ? (left * 2) : (right * 2);
                    Console.WriteLine($"Odd {highestOdd}");

                } else if (right == left || left == right)
                {
                    Console.WriteLine($"Even {left + right}");
                }
            }
        }
    }
}