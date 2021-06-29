using MeetingManager.Core.Entities;
using MeetingManager.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace MeetingManager.Core.Cache
{
    public class MeetingCache
    {
        private IMemoryCache<List<Meeting>> cache;
        private IMeetingRepository meetingRepository;

        public MeetingCache(IMemoryCache<List<Meeting>> cache, IMeetingRepository meetingRepository)
        {
            this.cache = cache;
            this.meetingRepository = meetingRepository;
        }

        public async Task<List<Meeting>> GetMeetings()
        {
            if(!cache.TryGetValue("meetings", out List<Meeting> meetings))
            {
                meetings = await SetCache();
            }
            return meetings;
        }

        public async Task DeleteMeeting(int id)
        {
            if(!cache.TryGetValue("meetings", out List<Meeting> meetings))
            {
                meetings = await SetCache();
                return;
            }
            meetings.Remove(meetings.FirstOrDefault(m => m.Id == id));
            cache.Set("meetings", meetings);
        }

        public async Task InsertMeeting(Meeting meeting)
        {
            if(!cache.TryGetValue("meetings", out List<Meeting> meetings))
            {
                meetings = await SetCache();
                return;
            }
            meetings.Add(meeting);
            cache.Set("meetings", meetings);
        }

        public async Task UpdateMeeting(Meeting meeting)
        {
            if(!cache.TryGetValue("meetings", out List<Meeting> meetings))
            {
                meetings = await SetCache();
                return;
            }
            meetings[meetings.IndexOf(meetings.FirstOrDefault(m => m.Id == meeting.Id))] = meeting;
        }

        private async Task<List<Meeting>> SetCache()
        {
            var meetings = await meetingRepository.GetAllAsync();
            cache.Set("meetings", meetings);
            return meetings;
        }
    }
}
