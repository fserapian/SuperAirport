using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIAeroport.Models;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using System.Globalization;

namespace APIAeroport.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly AeroportContext _context;

        public NotificationsController(AeroportContext context)
        {
            _context = context;
        }

        // GET: api/Notifications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notification>>> GetNotifications()
        {
            return await _context.Notifications.ToListAsync();
        }

        // // GET: api/Notifications/5
        // [HttpGet("{id}")]
        // public async Task<ActionResult<Notification>> GetNotification(int id)
        // {
        //     /*var notification = await _context.Notifications.FindAsync(id);

        //     if (notification == null)
        //     {
        //         return NotFound();
        //     }

        //     return notification;*/
        //     return null;
        // }

        // // PUT: api/Notifications/5
        // // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // // more details see https://aka.ms/RazorPagesCRUD.
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutNotification(int id, Notification notification)
        // {/*
        //     if (id != notification.VolCeduleId)
        //     {
        //         return BadRequest();
        //     }

        //     _context.Entry(notification).State = EntityState.Modified;

        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!NotificationExists(notification))
        //         {
        //             return NotFound();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }

        //     return NoContent();*/
        //     return null;
        // }

        // POST: api/Notifications
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Notification>> PostNotification(Notification notification)
        {
            const string accountSid = "YOUR_ACCOUNT_SID";
            const string authToken = "YOUR_AUTH_TOKEN";

            //username: avyay.zimerent@opka.org
            //password: avyay.zimerent12
            VolCedule volCed = _context.VolCedules.Single(y => y.VolCeduleId == notification.VolCeduleId);
            VolGenerique volGen = _context.VolGeneriques.Single(x => x.VolGeneriqueId == volCed.VolGeneriqueId);
            Aeroport aeroport = _context.Aeroports.Single(x => x.AeroportId == volGen.AeroportId);
            DateTime dateVolPrevue = new DateTime(volCed.DatePrevue.Year, volCed.DatePrevue.Month, volCed.DatePrevue.Day, volGen.HeurePrevue.Hour, volGen.HeurePrevue.Minute, 0);
            int finFR = 0;
            notification.DateInscription = DateTime.Now;
            //message FR
            string messageText = "\nEnglish will follow\n\n";
            messageText += "Ce numéro a été inscrit à la liste de diffusion du vol " + volGen.VolGeneriqueId;
            if (volGen.Direction)
                messageText += ", arrivant de " + aeroport.Ville + ", " + aeroport.Pays;
            else
                messageText += " à destination de " + aeroport.Ville + ", " + aeroport.Pays;
            messageText += " le " + dateVolPrevue.ToString("f", CultureInfo.CreateSpecificCulture("fr-CA"));
            messageText += ". Pour vous désinscrire envoyez un message contenant ceci: \"" + volCed.VolCeduleId + "stop\"\n\n";
            //message ENG
            messageText += "This number has been ";
            finFR = messageText.Length;
            messageText += "subscribed to the mailing list for the flight " + volGen.VolGeneriqueId;
            if (volGen.Direction)
                messageText += ", coming from " + aeroport.Ville + ", " + aeroport.Pays;
            else
                messageText += " going to " + aeroport.Ville + ", " + aeroport.Pays;
            messageText += " on " + dateVolPrevue.ToString("f", CultureInfo.CreateSpecificCulture("en-US"));
            messageText += ". To unsubscribe please send a message containing the following: \"" + volCed.VolCeduleId + "stop\"";

            _context.Notifications.Add(notification);
            try
            {
                await _context.SaveChangesAsync();
                TwilioClient.Init(accountSid, authToken);
                var message = MessageResource.Create(
                    body: messageText,
                    from: new Twilio.Types.PhoneNumber("+YOUR_TWILIO_NUMBER"),
                    to: new Twilio.Types.PhoneNumber("+1" + notification.NumTel)
                    );
                Console.WriteLine(message.Sid);
            }
            catch (DbUpdateException)
            {
                if (NotificationExists(notification))
                {
                    _context.Notifications.Remove(notification);
                    notification = _context.Notifications.Single(n => n.VolCeduleId == notification.VolCeduleId && n.NumTel == notification.NumTel);
                    if (notification.DateArret != null)
                    {
                        notification.DateArret = null;
                        _context.SaveChanges();
                        messageText = finFR + messageText.Substring(0, 38) + "ré" + messageText.Substring(38, finFR-38) + "re" + messageText.Substring(finFR);
                        TwilioClient.Init(accountSid, authToken);
                        var message = MessageResource.Create(
                            body: messageText,
                            from: new Twilio.Types.PhoneNumber("+YOUR_TWILIO_NUMBER"),
                            to: new Twilio.Types.PhoneNumber("+1" + notification.NumTel)
                            );
                        Console.WriteLine(message.Sid);
                        return null;
                    }
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return null;
            // return CreatedAtAction("GetNotification", new { id = notification.VolCeduleId }, notification);
        }

        // // DELETE: api/Notifications/5
        // [HttpDelete("{id}")]
        // public async Task<ActionResult<Notification>> DeleteNotification(int id)
        // {/*
        //     var notification = await _context.Notifications.FindAsync(id);
        //     if (notification == null)
        //     {
        //         return NotFound();
        //     }

        //     _context.Notifications.Remove(notification);
        //     await _context.SaveChangesAsync();

        //     return notification;*/
        //     return null;
        // }

        private bool NotificationExists(Notification notif)
        {
            return _context.Notifications.Any(e => e.VolCeduleId == notif.VolCeduleId && e.NumTel == notif.NumTel);
        }
    }
}
