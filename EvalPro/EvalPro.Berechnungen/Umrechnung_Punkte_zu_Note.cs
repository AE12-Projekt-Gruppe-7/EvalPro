using System;

namespace EvalPro.Berechnungen
{
    public static class IHKNotenschluessel
    {
        public static double BerechneNote(int punkte)
        {
            if (punkte == 100) return 1.0;
            else if (punkte < 100 && punkte > 97) return 1.1;
            else if (punkte <= 97 && punkte > 95) return 1.2;
            else if (punkte <= 95 && punkte > 93) return 1.3;
            else if (punkte <= 93 && punkte > 91) return 1.4;
            else if (punkte == 91) return 1.5;
            else if (punkte == 90) return 1.6;
            else if (punkte == 89) return 1.7;
            else if (punkte == 88) return 1.8;
            else if (punkte == 87) return 1.9;
            else if (punkte <= 85 && punkte > 84) return 2.0;
            else if (punkte == 84) return 2.1;
            else if (punkte == 83) return 2.2;
            else if (punkte == 82) return 2.3;
            else if (punkte == 81) return 2.4;
            else if (punkte <= 80 && punkte > 78) return 2.5;
            else if (punkte == 78) return 2.6;
            else if (punkte == 77) return 2.7;
            else if (punkte <= 76 && punkte > 74) return 2.8;
            else if (punkte == 74) return 2.9;
            else if (punkte <= 73 && punkte > 71) return 3.0;
            else if (punkte == 71) return 3.1;
            else if (punkte == 70) return 3.2;
            else if (punkte <= 69 && punkte > 67) return 3.3;
            else if (punkte == 67) return 3.4;
            else if (punkte <= 66 && punkte > 64) return 3.5;
            else if (punkte <= 64 && punkte > 62) return 3.6;
            else if (punkte == 62) return 3.7;
            else if (punkte <= 61 && punkte > 58) return 3.8;
            else if (punkte <= 58 && punkte > 57) return 3.9;
            else if (punkte <= 57 && punkte > 55) return 4.0;
            else if (punkte == 55) return 4.1;
            else if (punkte <= 54 && punkte > 52) return 4.2;
            else if (punkte <= 52 && punkte > 50) return 4.3;
            else if (punkte == 50) return 4.4;
            else if (punkte <= 49 && punkte > 47) return 4.5;
            else if (punkte <= 49 && punkte > 45) return 4.6;
            else if (punkte <= 45 && punkte > 43) return 4.7;
            else if (punkte <= 43 && punkte > 41) return 4.8;
            else if (punkte <= 41 && punkte > 39) return 4.9;
            else if (punkte <= 39 && punkte > 37) return 5.0;
            else if (punkte <= 37 && punkte > 35) return 5.1;
            else if (punkte <= 35 && punkte > 33) return 5.2;
            else if (punkte <= 33 && punkte > 31) return 5.3;
            else if (punkte <= 31 && punkte > 29) return 5.4;
            else if (punkte <= 29 && punkte > 24) return 5.5;
            else if (punkte <= 24 && punkte > 19) return 5.6;
            else if (punkte <= 19 && punkte > 14) return 5.7;
            else if (punkte <= 14 && punkte > 9) return 5.8;
            else if (punkte <= 9 && punkte > 4) return 5.9;
            else if (punkte <= 4 && punkte >= 0) return 6.0;
            else return -1; // Ungültige Punktzahl
        }
        
    }
}
