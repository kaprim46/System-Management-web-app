using System;
namespace Gestion_Parc.ViewModel
{
	public class Helper
	{
        public const string Success = "success";
        public const string Error = "error";
        public const string MsgType = "msgType";
        public const string Title = "title";
        public const string Msg = "msg";

        public const string Save = "Save";
        public const string Update = "Update";
        public const string Delete = "Delete";

        

        public enum Os
        {
            Windows,
            Linux,
            MacOS
        }

        public enum Cpu
        {
            Intel,
            AMD,
            M
        }

        public enum Color
        {
            Blanc,
            Noire,
            Rouge,
            Gris,
            Vert,
            Jaune,
            Bleu
        }

        public enum Technology
        {
            Inkjet,
            Thermal,
            Laser
        }

        public enum Output
        {
            Couleur,
            Noire,
            Monochrome
        }

        public enum Category
        {
            Computer,
            Printer,
            Server,
            Other
        }

        public enum BreackDownType
        {
            Matriel,
            Logiciel,
            Reseau,
            Autre
        }

        public enum Status
        {
            Reported,
            InProgress,
            Completed
        }

        public static List<string> ScreenResolution = new List<string>
        {
            "1280 x 720", "1920 x 1080", "2560 x 1440", "3840 x 2160"
        };

        public static List<string> GraphicsCard = new List<string>
        {
            "NVIDIA GeForce GTX", "Radeon", "Nvidia", "AMD"
        };

        public static List<string> MaintenanceType = new List<string>
        {
            "Maintenance de routine", "Maintenance preventive", "Entretien d'urgence"
        };

        

        
    }
}

