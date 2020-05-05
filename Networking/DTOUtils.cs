using app.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace networking
{
    public class DTOUtils
    {

        public static Employee getFromDTO(UserDTO usdto)
        {
            int id = usdto.Id;
            String pass = usdto.Password;
            String nume = usdto.Username;
            Employee client = new Employee() { Id = id, Username = nume, Password = pass };
            return client;
        }
        public static UserDTO getDTO(Employee user)
        {
            int id = user.Id;
            String pass = user.Password;
            String nume = user.Username;
            return new UserDTO(id, nume,pass);
        }

        public static UserDTO[] getDTO(Employee[] users)
        {
            UserDTO[] frDTO = new UserDTO[users.Length];
            for (int i = 0; i < users.Length; i++)
                frDTO[i] = getDTO(users[i]);
            return frDTO;
        }

        public static Employee[] getFromDTO(UserDTO[] users)
        {
            Employee[] friends = new Employee[users.Length];
            for (int i = 0; i < users.Length; i++)
            {
                friends[i] = getFromDTO(users[i]);
            }
            return friends;
        }


        public static Show getFromDTO(ShowDTO showDTO)
        {
            return new Show()
            {
                Id = showDTO.Id,
                Location = showDTO.Location,
                ShowDateTime = showDTO.ShowDateTime,
                NrAvailableSeats = showDTO.NrAvailableSeats,
                NrSoldSeats = showDTO.NrSoldSeats,
                ArtistName = showDTO.ArtistName
            };
        }

        public static Show[] getFromDTO(ShowDTO[] showDTOs)
        {
            Show[] shows= new Show[showDTOs.Length];
            for (int i = 0; i < showDTOs.Length; i++)
            {
                shows[i] = getFromDTO(showDTOs[i]);
            }
            return shows;
        }
        public static ShowDTO getDTO(Show show)
        {
            return new ShowDTO(show.Id, show.Location, show.ShowDateTime, show.NrAvailableSeats, show.NrSoldSeats, show.ArtistName);
        }

        public static ShowDTO[] getDTO(Show[] shows)
        {
            ShowDTO[] showsDTO = new ShowDTO[shows.Length];
            for (int i = 0; i < shows.Length; i++)
            {
                showsDTO[i] = getDTO(shows[i]);
            }
            return showsDTO;
        }



        public static Ticket[] getFromDTO(TicketDTO[] TicketeDTOs)
        {
            Ticket[] Tickete = new Ticket[TicketeDTOs.Length];
            for (int i = 0; i < TicketeDTOs.Length; i++)
            {
                Tickete[i] = getFromDTO(TicketeDTOs[i]);
            }
            return Tickete;
        }
        public static TicketDTO[] getDTO(Ticket[] Tickete)
        {
            TicketDTO[] Ticketdtos = new TicketDTO[Tickete.Length];
            for (int i = 0; i < Tickete.Length; i++)
            {
                Ticketdtos[i] = getDTO(Tickete[i]);
            }
            return Ticketdtos;
        }
        public static TicketDTO getDTO(Ticket ticket)
        {
            return new TicketDTO(ticket.Id, ticket.NrSeatsWanted, ticket.BuyerName, ticket.IdShow);
        }
        public static Ticket getFromDTO(TicketDTO t)
        {
            return new Ticket() { Id = t.Id, IdShow = t.IdShow, BuyerName = t.BuyerName, NrSeatsWanted = t.NrSeatsWanted };
        }



        public static Artist[] getFromDTO(ArtistDTO[] ArtisteDTOs)
        {
            Artist[] Artiste = new Artist[ArtisteDTOs.Length];
            for (int i = 0; i < ArtisteDTOs.Length; i++)
            {
                Artiste[i] = getFromDTO(ArtisteDTOs[i]);
            }
            return Artiste;
        }
        public static ArtistDTO[] getDTO(Artist[] Artiste)
        {
            ArtistDTO[] Artistdtos = new ArtistDTO[Artiste.Length];
            for (int i = 0; i < Artiste.Length; i++)
            {
                Artistdtos[i] = getDTO(Artiste[i]);
            }
            return Artistdtos;
        }
        public static ArtistDTO getDTO(Artist artist)
        {
            return new ArtistDTO(artist.Id, artist.Name);
        }
        public static Artist getFromDTO(ArtistDTO a)
        {
            return new Artist() { Id=a.Id, Name=a.Name };
        }


        public static Buyer[] getFromDTO(BuyerDTO[] BuyereDTOs)
        {
            Buyer[] Buyere = new Buyer[BuyereDTOs.Length];
            for (int i = 0; i < BuyereDTOs.Length; i++)
            {
                Buyere[i] = getFromDTO(BuyereDTOs[i]);
            }
            return Buyere;
        }
        public static BuyerDTO[] getDTO(Buyer[] Buyere)
        {
            BuyerDTO[] Buyerdtos = new BuyerDTO[Buyere.Length];
            for (int i = 0; i < Buyere.Length; i++)
            {
                Buyerdtos[i] = getDTO(Buyere[i]);
            }
            return Buyerdtos;
        }
        public static BuyerDTO getDTO(Buyer b)
        {
            return new BuyerDTO(b.Id, b.Name);
        }
        public static Buyer getFromDTO(BuyerDTO a)
        {
            return new Buyer() { Id = a.Id, Name = a.Name };
        }
    }
}
