using PintoChatNS.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PintoChatNS.Misc
{
    public class LoadingAnimation
    {
        private bool isRunning = false;
        private Thread thread;
        private int delayBetweenFrame;
        private int delayBeforeStartOver;
        private int loadingDots;
        private bool finishSuccessfully;
        private bool finishAlmostSuccessfully;

        public LoadingAnimation(int delayBetweenFrame, int delayBeforeStartOver, int loadingDots) 
        {
            this.delayBetweenFrame = delayBetweenFrame;
            this.delayBeforeStartOver = delayBeforeStartOver;
            this.loadingDots = loadingDots;
            thread = new Thread(new ThreadStart(Thread_Func));
        }

        private void Thread_Func() 
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(" [");
            for (int clearLoadingBarDot = 0; clearLoadingBarDot < loadingDots; clearLoadingBarDot++)
                Console.Write(" ");
            Console.Write("]");
            Console.SetCursorPosition(Console.CursorLeft - (loadingDots + 1), Console.CursorTop);

            int currentDotIndex = 0;
            bool finishWithError = false;

            while (true)
            {
                try
                {
                    if (currentDotIndex != loadingDots - 1 && isRunning)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(".");

                        Thread.Sleep(delayBetweenFrame);
                        currentDotIndex++;
                    }
                    else if (isRunning)
                    {
                        Console.Write(".");

                        Thread.Sleep(delayBeforeStartOver);
                        currentDotIndex = 0;

                        Console.SetCursorPosition(Console.CursorLeft - loadingDots, Console.CursorTop);
                        for (int clearLoadingBarDot = 0; clearLoadingBarDot < loadingDots; clearLoadingBarDot++)
                            Console.Write(" ");
                        Console.SetCursorPosition(Console.CursorLeft - loadingDots, Console.CursorTop);
                    }
                    else
                        break;
                }
                catch
                {
                    finishWithError = true;
                    break;
                }
            }

            try
            {
                if (currentDotIndex == (loadingDots - 1))
                    Console.SetCursorPosition(Console.CursorLeft - (loadingDots - 1), Console.CursorTop);
                else
                    Console.SetCursorPosition(Console.CursorLeft - currentDotIndex, Console.CursorTop);
            }
            catch 
            {
                finishWithError = true;
            }

            if (!finishWithError)
            {
                for (int finishedLoadingDotIndex = 0; finishedLoadingDotIndex < loadingDots; finishedLoadingDotIndex++)
                    if (finishedLoadingDotIndex == (loadingDots / 2))
                    {
                        if (finishSuccessfully)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("√");
                            continue;
                        }
                        else if (finishAlmostSuccessfully)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("!");
                            continue;
                        }

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("X");
                    }
                    else
                        Console.Write(" ");

                Console.Write(Environment.NewLine);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Unable to complete loading animation!");
                Console.ForegroundColor = ConsoleColor.Gray;
                return;
            }
        }

        public void Start()
        {
            isRunning = true;
            finishSuccessfully = false;
            finishAlmostSuccessfully = false;
            thread.Start();
        }

        private void Stop() 
        {
            isRunning = false;
        }

        [Obsolete()]
        public void ForceStop() 
        {
            Stop();
        }

        public void Finish(bool successfully, bool almostSuccessfully) 
        {
            finishSuccessfully = successfully;
            finishAlmostSuccessfully = almostSuccessfully;
            Stop();
        }
    }
}
