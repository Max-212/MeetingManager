using MeetingManager.Core.Entities;
using MeetingManager.Core.Interfaces;
using MeetingManager.Infastructure.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MeetingManager.Infrastructure.Repositories
{
    public class MeetingRepository : IMeetingRepository
    {
        private ApplicationContext db;

        public MeetingRepository(ApplicationContext context)
        {
            db = context;
        }

        public async Task<Meeting> CreateAsync(Meeting meetingData)
        {
            var meeting = db.Meetings.Add(meetingData).Entity;
            await db.SaveChangesAsync();
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
        }

        public async Task<List<Meeting>> GetAllAsync()
        {
            return await db.Meetings.Include(m => m.Partitipants).ToListAsync();
        }

        public async Task<Meeting> GetOneAsync(int id)
        {
            return await getMeetingWithPartitipants(id);
        }

        public async Task<Meeting> RemovePartitipantAsync(int meetingId, int userId)
        {
            var meeting = await getMeetingWithPartitipants(meetingId);
            if(meeting != null)
            {
                var user = meeting.Partitipants.FirstOrDefault(p => p.Id == userId);
                if(user != null)
                {
                    meeting.Partitipants.Remove(user);
                }
                await db.SaveChangesAsync();
                return meeting;
            }
            return null;
        }

        public async Task<Meeting> AddPartitipantAsync(int meetingId, int userId)
        {
            var meeting = await getMeetingWithPartitipants(meetingId);
            if(meeting != null )
            {
                var user = await db.Users.FirstOrDefaultAsync(u => u.Id == userId);
                if(user != null && !meeting.Partitipants.Contains(user))
                {
                    meeting.Partitipants.Add(user);
                }
                await db.SaveChangesAsync();
                return meeting;
            }
            return null;
        }

        public async Task<Meeting> UpdateAsync(Meeting meetingData)
        {
            var meeting = await db.Meetings.FirstOrDefaultAsync(m => m.Id == meetingData.Id);
            if (meeting == null)
            {
                return null;
            }
            meeting.Description = meetingData.Description;
            meeting.From = meetingData.From;
            meeting.Till = meetingData.Till;
            db.Meetings.Update(meeting);
            await db.SaveChangesAsync();
            return meeting;
        }
        
        private async Task<Meeting> getMeetingWithPartitipants(int id)
        {
            return await db.Meetings.Include(m => m.Partitipants)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

    }
}
