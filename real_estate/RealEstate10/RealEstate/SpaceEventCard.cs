namespace RealEstate {
    public class SpaceEventCard : Space {
        public EventCard.EventCardType eventcardtype;

        public SpaceEventCard(EventCard.EventCardType eventcardtype) {
            this.eventcardtype = eventcardtype;
            

        }

        public string getName() {
            if (eventcardtype == EventCard.EventCardType.Happenstance) {
                return "Happenstance";
            } else if (eventcardtype == EventCard.EventCardType.MysteryVault) {
                return "Mystery Vault";
            } else {
                return "";
            }

        }
    }
}
