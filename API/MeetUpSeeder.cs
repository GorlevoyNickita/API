using API.Entities;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System;

namespace API
{

    public class MeetUpSeeder
    {
        private readonly MeetUpContext _meetUpContext;
        public MeetUpSeeder(MeetUpContext meetupContext)
        {
            _meetUpContext = meetupContext;
        }

        public void Seed()
        {
            if (_meetUpContext.Database.CanConnect())
            {
                if (!_meetUpContext.MeetUps.Any())
                {
                    InsertSampleData();
                }
            }
        }
        private void InsertSampleData() 
        {
            var meetups = new List<MeetUp>
            {
                new MeetUp
                {
                    Name = "Web summit",
                    Date = DateTime.Now.AddDays(7),
                    IsPrivate = false,
                    Organizer = "Microsoft",
                    Location = new Location
                    {
                        City = "Krakow",
                        Street = "Szeroka 33/5",
                        PostCode = "31-337"
                    },
                    Lectures = new List<Lecture>
                    {
                        new Lecture
                        {
                            Author = "Bob Clark",
                            Topic = "Modern browsers",
                            Description = "Deep dive into V8"
                        }
                    }
                },
                new MeetUp
                {
                    Name = "4Devs",
                    Date = DateTime.Now.AddDays(7),
                    IsPrivate = false,
                    Organizer = "KGD",
                    Location = new Location
                    {
                        City = "Warszawa",
                        Street = "Chmielna 33/5",
                        PostCode = "00-007"
                    },
                    Lectures = new List<Lecture>
                    {
                        new Lecture
                        {
                            Author = "Will Smith",
                            Topic = "React.js",
                            Description = "Redux introduction"
                        },
                        new Lecture
                        {
                            Author = "John Cena",
                            Topic = "Angular store",
                            Description = "Ngxs in practice"
                        }
                    }
                },
            };
            _meetUpContext.AddRange(meetups);
            _meetUpContext.SaveChanges();
        }
    }
} 
