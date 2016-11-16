using Model.EnumTypes;
using System.ComponentModel;
using System;
using System.IO;
/**
* Jens Malm 
* 2016-09
* BlackJackGame
**/
namespace Model.GameObjects
{
    public class Card
    {
        private string FolderPath = "CardImages/";
        private string FileExtension = ".png";
        private int _value;
        public ImageFileSuitDefinition FileSuitCharacter { get; set; }
        public ImageFileFaceDefinition FileFaceCharacter { get; set; }
        public SuitType Suit { get; set; }
        public FaceType Face { get; set; }
        public int Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (value == 1 || value > 10)
                {
                    Face = (FaceType)Enum.Parse(typeof(FaceType), value.ToString());
                }

                _value = value;

            }
        }
        public bool HideCard { get; set; }

        public int CardNumber { get; set; }

        public string CardImageName
        {
            get
            {
                if (HideCard)
                {
                    return FolderPath + "b1fv" + FileExtension;
                }
                else
                {
                    return FolderPath + FileSuitCharacter + (FileFaceCharacter == 0 ? Value.ToString() : FileFaceCharacter.ToString()) + FileExtension;
                }
            }
        }
        public string GameCard
        {
            get
            {
                return Suit.ToString() + " " + Value.ToString();
            }
        }

    }

}
