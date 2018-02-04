using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.NetConnect.Models
{
    public class FbNews
    {
        public String message { get; set; }
        public DateTime created_time { get; set; }
        public String story { get; set; }
        public String full_picture { get; set; }
        public String permalink_url { get; set; }
        public String id { get; set; }
        public FbLikes likes { get; set; }
        //comments: {
            //data: [
                //{
                    //comments: {
                        //data: [
                            //{
                                //from: {
                                    //name: "Net.Connect",
                                    //id: "1399354000332089"
                                //},
                                //message: "Danke! :)",
                                //permalink_url: "https://www.facebook.com/netconnectev/posts/1971316499802500?comment_id=1971378426462974&reply_comment_id=1971379213129562",
                                //created_time: "2017-10-29T18:14:38+0000",
                                //id: "1971316499802500_1971379213129562"
                            //}
                        //],
                        //paging: {
                            //cursors: {
                                //before: "WTI5dGJXVnVkRjlqZAFhKemIzSTZANVGszTVRNM09USXhNekV5T1RVMk1qb3hOVEE1TXpBd09EYzQZD",
                                //after: "WTI5dGJXVnVkRjlqZAFhKemIzSTZANVGszTVRNM09USXhNekV5T1RVMk1qb3hOVEE1TXpBd09EYzQZD"
                            //}
                            //}
                        //},
                        //message: "Sehr coole Aktion",
                        //from: {
                            //name: "Bernd Unger",
                            //id: "1847871105239699"
                        //},
                        //permalink_url: "https://www.facebook.com/netconnectev/posts/1971316499802500?comment_id=1971378426462974",
                        //created_time: "2017-10-29T18:11:49+0000",
                        //id: "1971316499802500_1971378426462974"
                        //},
                        //{
                            //message: "Wäre gern dabei, ist leider etwas weit weg :/",
                            //from: {
                                //name: "MOritz Pserator",
                                //id: "145520466179825"
                            //},
                        //permalink_url: "https://www.facebook.com/netconnectev/posts/1971316499802500?comment_id=1971451866455630",
                        //created_time: "2017-10-29T23:30:15+0000",
                        //id: "1971316499802500_1971451866455630"
                    //}
                //],
            //paging: {
                //cursors: {
                    //before: "WTI5dGJXVnVkRjlqZAFhKemIzSTZANVGszTVRNM09EUXlOalEyTWprM05Eb3hOVEE1TXpBd056RXcZD",
                    //after: "WTI5dGJXVnVkRjlqZAFhKemIzSTZANVGszTVRRMU1UZAzJOalExTlRZAek1Eb3hOVEE1TXpFNU9ERTIZD"
                //}
            //}
        //}
    }

    public class FbLikes
    {
        public List<FbLikeUser> data { get; set; }
        public FbPaging paging { get; set; }
        
        public class FbLikeUser
        {
            public String id { get; set; }
            public String name { get; set; }
        }
    }

    public class FbPaging
    {
        public FbLikePagingCursor cursors { get; set; }

        public class FbLikePagingCursor
        {
            public String before { get; set; }
            public String after { get; set; }
        }
    }
}