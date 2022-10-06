using System;

namespace lab_2._1
{
    class Pupil
    {
        public virtual void Study()
        {
            Console.WriteLine("Учится");
        }

        public virtual void Read()
        {
            Console.WriteLine("Читает");
        }

        public virtual void Write()
        {
            Console.WriteLine("Пишет");
        }

        public virtual void Relax()
        {
            Console.WriteLine("Отдыхает");
        }
    }

    class ExcelentPupil : Pupil
    {
        public override void Study()
        {
            Console.WriteLine("Отлично учится");
        }

        public override void Read()
        {
            Console.WriteLine("Отлично читает");
        }

        public override void Write()
        {
            Console.WriteLine("Но коряво пишет");
        }

        public override void Relax()
        {
            Console.WriteLine("Отдыхает тоже так себе");
        }
    }

    class GoodPupil : Pupil
    {
        public override void Study()
        {
            Console.WriteLine("Хорошо учится");
        }

        public override void Read()
        {
            Console.WriteLine("Хорошо читает");
        }

        public override void Write()
        {
            Console.WriteLine("Пишет неплохо");
        }

        public override void Relax()
        {
            Console.WriteLine("В отличии от некоторых отдыхает");
        }
    }

    class BadPupil : Pupil
    {
        public override void Study()
        {
            Console.WriteLine("Плохо учится");
        }

        public override void Read()
        {
            Console.WriteLine("Мало читает");
        }

        public override void Write()
        {
            Console.WriteLine("Пишет (любовные письма)");
        }

        public override void Relax()
        {
            Console.WriteLine("Отдыхает всегда!!");
        }
    }

    class ClassRoom
    {
        private Pupil[] p = new Pupil[4];

        public ClassRoom(params Pupil[] pupils)
        {
            for (int i = 0; i < pupils.Length; ++i)
            {
                p[i] = pupils[i];
            }
        }

        public void InfoClassRoom()
        {
            for (int i = 0; i < 4; ++i)
            {
                Console.WriteLine("\n" + p[i].GetType() + " : ");
                p[i].Study();
                p[i].Read();
                p[i].Write();
                p[i].Relax();
            }
        }
    }

    internal class Program
    {
        public static void Main(string[] args)
        {
            ClassRoom cRoom1 = new ClassRoom(new ExcelentPupil(), new GoodPupil(), new BadPupil(), new BadPupil());
            cRoom1.InfoClassRoom();
            Console.WriteLine("\n\n\n");
            ClassRoom cRoom2 = new ClassRoom(new ExcelentPupil(), new GoodPupil(), new BadPupil());
            cRoom2.InfoClassRoom();
            Console.WriteLine("\n\n\n");
            ClassRoom cRoom3 = new ClassRoom(new ExcelentPupil(), new GoodPupil());
            cRoom3.InfoClassRoom();
            Console.ReadLine();
        }
    }
}