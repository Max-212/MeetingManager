using MeetingManager.Core.Entities;
using MeetingManager.Core.Interfaces;
using MeetingManager.Infastructure.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace MeetingManager.Infrastructure.Repositories
{
    public class MeetingRepository : IMeetingRepository
    {
        private ApplicationContext db;
        private IMemoryCache cache;

        public MeetingRepository(ApplicationContext context, IMemoryCache cache)
        {
            db = context;
            this.cache = cache;
        }

        public async Task<Meeting> CreateAsync(Meeting meetingData)
        {
            var meeting = new Meeting()
            {
                Description = meetingData.Description,
                From = meetingData.From,
                Till = meetingData.Till,
                Participants = new List<User>()
            };
            db.Meetings.Add(meeting);
            await addParticipants(meeting, meetingData.Participants);
            await db.SaveChangesAsync();
            if(!cache.TryGetValue("meetings", out List<Meeting> meetings))
            {
                meetings = await setCache();
                return meeting;
            }
            meetings.Add(meeting);
            cache.Set("meetings", meetings);
            return meeting;
        }

        public async Task DeleteAsync(int id)
        {
            var meeting = await db.Meetings.FirstOrDefaultAsync(m => m.Id == id);
            if(meeting == null)
            {
                return;
            }
            db.Meetings.Remove(meeting);
            await db.SaveChangesAsync();
            if (!cache.TryGetValue("meetings", out List<Meeting> meetings))
            {
                meetings = await setCache();
                return;
            }
            meetings.Remove(meetings.FirstOrDefault(m => m.Id == meeting.Id));
            cache.Set("meetings", meetings);
        }

        public async Task<List<Meeting>> GetAllAsync()
        {
            if(!cache.TryGetValue("meetings", out List<Meeting> meetings))
            {
                meetings = await setCache();
            }
            return meetings;
        }

        public async Task<Meeting> GetOneAsync(int id)
        {
            return await getMeetingWithParticipants(id);
        }

        public async Task<Meeting> UpdateAsync(Meeting meetingData)
        {
            var meeting = await getMeetingWithParticipants(meetingData.Id);
            if (meeting == null)
            {
                return null;
            }
            meeting.Description = meetingData.Description;
            meeting.From = meetingData.From;
            meeting.Till = meetingData.Till;
            meeting.Participants.Clear();
            await addParticipants(meeting, meetingData.Participants);
            db.Meetings.Update(meeting);
            await db.SaveChangesAsync();
            if (!cache.TryGetValue("meetings", out List<Meeting> meetings))
            {
                meetings = await setCache();
                return meeting;
            }
            meetings[meetings.IndexOf(meetings.FirstOrDefault(m => m.Id == meeting.Id))] = meeting;
            cache.Set("meetings", meetings);
            return meeting;
        }
        
        private async Task<Meeting> getMeetingWithParticipants(int id)
        {
            return await db.Meetings.Include(m => m.Participants)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        private async Task<List<Meeting>> setCache()
        {
            var meetings = await db.Meetings.Include(m => m.Participants).ToListAsync();
            cache.Set("meetings", meetings);
            return meetings;
        }

        private async Task<Meeting> addParticipants(Meeting meeting, List<User> participants)
        {
            foreach (var participant in participants)
            {
                var user = await db.Users.FirstOrDefaultAsync(u => u.Id == participant.Id);
                if (user != null && !meeting.Participants.Contains(user))
                {
                    meeting.Participants.Add(user);
                }
            }
            return meeting;
        }
    }
}
