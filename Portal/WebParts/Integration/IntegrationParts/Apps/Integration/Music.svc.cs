using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.Entity;

namespace WCMS.WebSystem.Apps.Integration
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMusicService" in both code and config file together.
    [ServiceContract]
    public interface IMusicService
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        IEnumerable<Music> GetMusicList(int categoryId = -2);

        [OperationContract]
        IEnumerable<MusicEntry> GetMusicEntryList(int musicId = -2);
    }

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MusicService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select MusicService.svc or MusicService.svc.cs at the Solution Explorer and start debugging.
    public class MusicService : IMusicService
    {
        public void DoWork()
        {
        }

        public IEnumerable<Music> GetMusicList(int categoryId = -2)
        {
            MusicEntities e = new MusicEntities();

            if (categoryId > -2)
                return e.Musics.Where(i => i.CategoryId == categoryId);

            return e.Musics;
        }

        public IEnumerable<MusicEntry> GetMusicEntryList(int musicId = -2)
        {
            MusicEntities e = new MusicEntities();

            if (musicId > -2)
                return e.MusicEntries.Where(i => i.MusicId == musicId);

            return e.MusicEntries;
        }
    }
}
