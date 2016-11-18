using Model.GameObjects;
/**
 * Jens Malm 
 * 2016-09
 * BlackJackGame
 **/
namespace Model.CommandInterfaces
{
    public interface IDeckCommands
    {
        Card FindCardByCardnumber(int cardNumber);
        Card GetCard();
        int GetCardsLeftInDeck();
        void ShuffleDeck();
    }
}