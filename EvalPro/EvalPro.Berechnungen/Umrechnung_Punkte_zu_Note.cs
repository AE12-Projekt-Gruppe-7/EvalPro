using System;

class IHKNotenschluessel
{
    static void Main()
    {
        Console.WriteLine("Bitte geben Sie die erreichte Punktzahl ein (0–100):");
        if (int.TryParse(Console.ReadLine(), out int punkte))
        {
            string note = BerechneNote(punkte);
            Console.WriteLine($"Die Note für {punkte} Punkte ist: {note}");
        }
        else
        {
            Console.WriteLine("Ungültige Eingabe. Bitte geben Sie eine ganze Zahl ein.");
        }
    }

    static string BerechneNote(int punkte)
    {
        if (punkte == 100){
            return note = 1.0;
        }
        elseif(punkte <100 && >97){
            return note = 1.1;
        }
        elseif(punkte <=97 && >95){
            return note = 1.2;
        }
        elseif(punkte <= && >93){
            return note = 1.3;
        }
        elseif(punkte <=93 && >91){
            return note = 1.4;
        }
        elseif(punkte == 91){
            return note = 1.5;
        }
        elseif(punkte == 90){
            return note = 1.6;
        }
        elseif(punkte == 89){
            return note = 1.7;
        }
        elseif(punkte == 88){
            return note = 1.8;
        }
        elseif(punkte == 87){
            return note = 1.9;
        }
        elseif(punkte <= 85 && >84){
            return note = 2.0;
        }
        elseif(punkte == 84){
            return note = 2.1;
        }
        elseif(punkte == 83){
            return note = 2.2;
        }
        elseif(punkte == 82){
            return note = 2.3;
        }
        elseif(punkte == 81){
            return note = 2.4;
        }
        elseif(punkte <= 80 && >78){
            return note = 2.5;
        }
        elseif(punkte == 78){
            return note = 2.6;
        }
        elseif(punkte == 77){
            return note = 2.7;
        }
        elseif(punkte <= 76 && >74){
            return note = 2.8;
        }
        elseif(punkte == 74){
            return note = 2.9;
        }
        elseif(punkte <= 73 && >71){
            return note = 3.0;
        }
        elseif(punkte == 71){
            return note = 3.1;
        }
        elseif(punkte == 70){
            return note = 3.2;
        }
        elseif(punkte <=69 && >67){
            return note = 3.3;
        }
        elseif(punkte == 67){
            return note = 3.4;
        }
        elseif(punkte <=66 && >64){
            return note = 3.5;
        }
        
    }
}
