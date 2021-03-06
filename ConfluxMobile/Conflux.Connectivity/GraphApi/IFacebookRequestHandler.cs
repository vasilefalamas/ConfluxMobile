﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Conflux.Connectivity.GraphApi
{
    public interface IFacebookRequestHandler
    {
        Task<string> GetUserNameInfoAsync();

        Task<string> GetLocationInfoAsync(long locationId);
        
        Task<string> GetProfilePictureAsync(PictureSize pictureSize = PictureSize.Size160x160);

        Task<string> GetEventsByKeywordAsync(string locationKeyword, uint offset = 0, uint? limit = null, DateTime? since = null, DateTime? until = null);

        Task<string> GetEventAsync(string eventId);

        Task<string> GetMyEvents();

        Task<string> GetEventPhotos(string eventId);
        
        Task<string> PostEventAttendance(string eventId);
    }
}
