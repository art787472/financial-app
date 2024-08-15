using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using System.Windows.Forms;
using 記帳程式.Services;
//using System.Timers;
//using Timer = System.Timers.Timer;

namespace 記帳程式.Utility
{
    internal static class Extension
    {
        private static System.Windows.Forms.Timer timer;
        public static void DebounceTime(this Form form, Action callback, double miliseconds)
        {
            
            if(timer != null)
            {
                timer.Stop();
                timer.Dispose();
                timer = null;
            }
            
            if(timer == null)
            {
                timer = new System.Windows.Forms.Timer();
                timer.Interval = (int)miliseconds;
                //timer.AutoReset = false;
                
                timer.Tick += (Object source, EventArgs e) => { 
                    callback.Invoke();
                    timer.Stop();
                    timer.Dispose();
                    timer = null;
                };
                timer.Start();
            }

            
        }

        private static System.Timers.Timer timer1;
        public static void DebounceTime1(this Form form, Action callback, double miliseconds)
        {

            if (timer1 != null)
            {
                timer1.Stop();
                timer1.Dispose();
                timer1 = null;
            }

            

            if (timer1 == null)
            {
                timer1 = new System.Timers.Timer();
                timer1.Interval = miliseconds;
                timer1.AutoReset = false;
                timer1.SynchronizingObject = form;

                timer1.Elapsed += (object sender, System.Timers.ElapsedEventArgs e) => 
                {
                    
                    callback.Invoke();
                    timer1.Dispose();
                    timer1 = null;
                };
                   
                timer1.Start();
            }


        }

        private static System.Threading.Timer timer2;

        public static void DebounceTime2(this Form form, Action callback, double miliseconds)
        {
            
            if(timer2 != null)
            {
                timer2.Change((int)miliseconds, Timeout.Infinite);
                return;

            }
            
           
            timer2 = new System.Threading.Timer(Callback, (form, callback), (int)miliseconds, Timeout.Infinite);
            
            
        }

        private static void Timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static void Callback(Object state)
        {
            (Form, Action) tuple = ((Form, Action))state;
            tuple.Item1.Invoke(new Action(() =>
            {
                tuple.Item2.Invoke();

            }));
        }

        public static void AddCustomColoums(this DataGridView dataGridView)
        {
            NoteService.AddImage(dataGridView);
            NoteService.AddComboboxColoumn(dataGridView);
            NoteService.AddDelete(dataGridView);
        }
    }
}
