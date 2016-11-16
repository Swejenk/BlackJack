using Model.GameObjects;

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