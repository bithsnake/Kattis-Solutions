using System;
using System.Linq;
using System.Collections.Generic;


namespace KattisProblem
{
    internal class ImageDecoding
    {

        public static void Main(string[] args)
        {
            const string PIXEL_A = "#";
            const string PIXEL_B = ".";
            const string ERROR_MESSAGE = "Error decoding image";
            bool canDoFirstCheck = true;
            string usedPixel = "";
            string errorCode = "";
            int count = 0;
            int sequences = 0;
            int totalScanLineLength = 0;
            int scanLineAmount = 0;
            int loopThrough = 0;
            int currentDecodedScanLineLength = 0;
            int maxDecodedScanLineLength = 0;
            List<string> image = new List<string>();

            string line;
            while ((line = Console.ReadLine()) != null)
            {
                string completedString = "";
                if (line == "0")
                {
                    break;
                }

                if(loopThrough > 0)
                {
                    loopThrough--;
                    continue;
                }

                if (!string.IsNullOrEmpty(line))
                {
                    int inputNumber = 0;
                    line = line.Trim();
                    var num = line.Split(' ');
                    bool inputNumberber = int.TryParse(num[0], out inputNumber);
                    // 
                    if (inputNumberber)
                    {
                        if(inputNumber > 100)
                        {
                            count = 0;
                            errorCode = "";
                            currentDecodedScanLineLength = 0;
                            canDoFirstCheck = true;
                            maxDecodedScanLineLength = 0;
                            totalScanLineLength = 0;
                            loopThrough = inputNumber;
                            continue;
                        }
                        if (inputNumberber && inputNumber == 0)
                        {
                            break;
                        }
                        totalScanLineLength = inputNumber;
                        continue;
                    }
                    // on first check if the first character is not a number, add the pixel that is going to be used later on.
                    if (!inputNumberber)
                    {
                        usedPixel = num[0];
                    }

                    // decode line scan length
                    if (totalScanLineLength > 0 && totalScanLineLength <= 100)
                    {
                        for (int i = 0; i < num.Length; i++)
                        {
                            int.TryParse(num[i], out inputNumber);
                            currentDecodedScanLineLength += inputNumber;
                            for (int m = 0; m < inputNumber; m++)
                            {
                                completedString += usedPixel;
                                if (m == inputNumber - 1)
                                {
                                    usedPixel = (usedPixel != PIXEL_B) ? PIXEL_B : PIXEL_A;
                                }
                            }
                        }
                        // set current decoded scanline léngth
                        if (canDoFirstCheck)
                        {
                            canDoFirstCheck = false;
                            maxDecodedScanLineLength = currentDecodedScanLineLength;
                        }

                        // if any new line is higher that the first decoded scanline length, set error message
                        if (currentDecodedScanLineLength > 0 && currentDecodedScanLineLength > maxDecodedScanLineLength && !canDoFirstCheck)
                        {
                            errorCode = "Error decoding image";
                        }

                        count++;
                        var o = new ImageRow() { ScanLineString = completedString };
                        image.Add(o.ScanLineString);
                        completedString = "";
                        currentDecodedScanLineLength = 0;

                        // when all lines have been gone through, set error message if there is any, and reset vars
                        if (count > 0 && totalScanLineLength > 0 && count >= totalScanLineLength)
                        {
                            if (errorCode != "")
                            {
                                image.Add(ERROR_MESSAGE);
                            }
                            image.Add("");
                            count = 0;
                            errorCode = "";
                            currentDecodedScanLineLength = 0;
                            canDoFirstCheck = true;
                            maxDecodedScanLineLength = 0;
                            totalScanLineLength = 0;
                        }

                    }
                }
                // end program
                if (line == "0" || line == null)
                {
                    break;
                }
            }
            // adjust last things and end program
            if (line == "0")
            {
                if(image[0] == "")
                {
                    image.RemoveAt(0);
                }
                if(image[image.Count - 1] == "")
                {
                    image.RemoveAt(image.Count - 1);
                }
                foreach (var _decodedScanLine in image)
                {
                    Console.WriteLine(_decodedScanLine);
                }
            }
        }

        public class ImageRow
        {
            public string ScanLineString { get; set; }
        }
    }
}