using MiniMusicPlayer.Models;
using MoreLinq;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace MiniMusicPlayer.ViewModels
{
    public class MusicPlayerViewModel : ViewModelBase
    {
        private WaveOutEvent outputDevice;
        private AudioFileReader audioFile;

        public Timer Timer = new Timer();

        public MusicPlayerViewModel()
        {
            outputDevice = new WaveOutEvent();

            PlaySongCommand = new RelayCommand(PlaySong);
            PauseSongCommand = new RelayCommand(PauseSong);
            PlayNextSongCommand = new RelayCommand(PlayNextSong);
            PlayPreviousSongCommand = new RelayCommand(PlayPreviousSong);
            ChangePlaybackModeCommand = new RelayCommand(ChangePlaybackMode);
            FilterSongsByArtistCommand = new RelayCommand(FilterSongsByArtist);

            Timer.Tick += Timer_Tick;

            Timer.Interval = 1;
            Timer.Start();

            LoadData();
        }

        public List<Song> AllSongsList { get; set; }
        public List<string> ArtistList { get; set; }

        public ICommand PlaySongCommand { get; set; }
        public ICommand PauseSongCommand { get; set; }
        public ICommand PlayNextSongCommand { get; set; }
        public ICommand PlayPreviousSongCommand { get; set; }
        public ICommand ChangePlaybackModeCommand { get; set; }
        public ICommand FilterSongsByArtistCommand { get; set; }

        public Song CurrentSong { get; set; }
        public string SelectedArtist { get; set; }

        public TimeSpan CurrentSongMaxTime { get; set; }
        public string CurrentSongCurrentTime { get; set; }

        public bool IsCurrentSongPlaying { get; set; }
        public bool IsPlaybackContinous { get; set; }

        public int CurrentSongMaxTimeTotalSeconds { get; set; }

        private int _currentSongCurrentTimeInSeconds;
        public int CurrentSongCurrentTimeInSeconds
        {
            get
            {
                return _currentSongCurrentTimeInSeconds;
            }
            set
            {
                if (_currentSongCurrentTimeInSeconds != value)
                {
                    _currentSongCurrentTimeInSeconds = value;

                    if (audioFile.CurrentTime.TotalMilliseconds != value)
                    {
                        audioFile.CurrentTime = new TimeSpan(0, 0, 0, 0, value);
                    }
                }
            }
        }

        public bool IsPlaybackLooped
        {
            get
            {
                return !IsPlaybackContinous;
            }
        }

        public List<string> MusicLibraryPaths
        {
            get
            {
                var result = new List<string>();

                var userPath = Environment.ExpandEnvironmentVariables("%userprofile%").Split(':').Last();

                foreach (var drive in DriveInfo.GetDrives())
                {
                    if (Directory.Exists(string.Format(@"{0}{1}\Music", drive.Name, userPath)))
                    {
                        result.Add(string.Format(@"{0}{1}\Music", drive.Name, userPath));
                    }
                }

                return result;
            }
        }

        public List<string> SupportedExtensions
        {
            get
            {
                return new List<string>() { "mp3", "x-mp3", "x-id3", "x-mpeg", "x-mpeg-3", "mpeg3", "m2a", "mp2", "mp1", "x-mp2", "x-mp1" };
            }
        }

        public string CurrentSongPlaying
        {
            get
            {
                return CurrentSong != null ? string.Format("{0} - {1} - {2}", CurrentSong.Title, CurrentSong.Album, CurrentSong.Artist) : string.Empty;
            }
        }

        public List<Song> CurrentSongList
        {
            get
            {
                return !string.IsNullOrEmpty(SelectedArtist) ? AllSongsList.Where(song => song.Artist == SelectedArtist).ToList() : AllSongsList;
            }
        }

        public string CurrentSongMaxTimeString
        {
            get
            {
                return CurrentSongMaxTime.Ticks > 0 ? string.Format("{0}:{1}", CurrentSongMaxTime.Minutes, CurrentSongMaxTime.Seconds) : string.Empty;
            }
        }

        public Visibility PlayButtonVisibility
        {
            get
            {
                return IsCurrentSongPlaying ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public Visibility PauseButtonVisibility
        {
            get
            {
                return IsCurrentSongPlaying ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public Visibility PlaybackLoopedButtonVisibility
        {
            get
            {
                return IsPlaybackLooped ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public Visibility PlaybackContinousButtonVisibility
        {
            get
            {
                return IsPlaybackContinous ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        #region Private Methods

        private void LoadData()
        {
            AllSongsList = new List<Song>();
            ArtistList = new List<string>();

            CurrentSongCurrentTimeInSeconds = 0;
            CurrentSongMaxTimeTotalSeconds = 1;

            IsPlaybackContinous = true;

            GetSongListAsync();

            AllSongsList = AllSongsList.OrderBy(song => song.Artist).ThenBy(song => song.Album).ThenBy(song => song.Track).ToList();

            ArtistList = AllSongsList.DistinctBy(song => song.Artist).OrderBy(song => song.Artist).Select(song => song.Artist).ToList();
        }

        private void GetSongListAsync()
        {
            var songList = new List<string>();

            foreach (var path in MusicLibraryPaths)
            {
                songList.AddRange(Directory.GetFiles(path, ".", SearchOption.AllDirectories));
            }

            foreach (var songPath in songList.Where(fileName => SupportedExtensions.Any(extension => fileName.Contains(extension))))
            {
                var tagLibSong = TagLib.File.Create(songPath);

                var songDetails = tagLibSong.Tag;

                var song = new Song
                {
                    Title = songDetails.Title,
                    Album = songDetails.Album,
                    Artist = songDetails.Performers.FirstOrDefault(),
                    Path = songPath,
                    Track = songDetails.Track,
                    TrackCount = songDetails.TrackCount
                };

                AllSongsList.Add(song);
                SelectedArtist = "All Songs";
            }
        }

        private void ChangeSongPosition()
        {
            outputDevice.Stop();
            Random random = new Random();
            audioFile.Position = random.Next(0, Convert.ToInt32(audioFile.Length));
            outputDevice.Play();
        }

        private void PlaySong(object obj)
        {
            if (obj != null)
            {
                IsCurrentSongPlaying = false;
                outputDevice.Stop();

                var songDetails = TagLib.File.Create(obj.ToString()).Tag;

                CurrentSong = new Song
                {
                    Title = songDetails.Title,
                    Album = songDetails.Album,
                    Artist = songDetails.Performers.FirstOrDefault(),
                    Path = obj.ToString()
                };

                audioFile = new AudioFileReader(obj.ToString());

                CurrentSongMaxTime = audioFile.TotalTime;

                outputDevice.Init(audioFile);
            }

            if (CurrentSong != null)
            {
                outputDevice.Play();
                IsCurrentSongPlaying = true;
            }
        }

        private void PauseSong(object obj)
        {
            IsCurrentSongPlaying = false;
            outputDevice.Pause();
        }

        private void PlayNextSong(object obj)
        {
            var positionInSongList = CurrentSongList.FindIndex(item => item.Path == CurrentSong.Path);

            if (positionInSongList <= CurrentSongList.Count())
            {
                IsCurrentSongPlaying = false;
                PlaySong(CurrentSongList.ElementAtOrDefault(positionInSongList + 1)?.Path);
            }
        }

        private void PlayPreviousSong(object obj)
        {
            var positionInSongList = CurrentSongList.FindIndex(item => item.Path == CurrentSong.Path);

            IsCurrentSongPlaying = false;

            if (positionInSongList > 0 && audioFile?.CurrentTime < new TimeSpan(0, 0, 3))
            {
                PlaySong(CurrentSongList.ElementAtOrDefault(positionInSongList - 1)?.Path);
            }
            else
            {
                PlaySong(CurrentSong.Path);
            }
        }

        private void ChangePlaybackMode(object obj)
        {
            IsPlaybackContinous = !IsPlaybackContinous;
        }

        private void FilterSongsByArtist(object obj)
        {
            SelectedArtist = obj as string;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (audioFile != null)
            {
                CurrentSongCurrentTime = string.Format("{0}:{1:d2}", audioFile?.CurrentTime.Minutes, audioFile?.CurrentTime.Seconds);
                CurrentSongMaxTimeTotalSeconds = Convert.ToInt32(audioFile?.TotalTime.TotalMilliseconds);
                CurrentSongCurrentTimeInSeconds = Convert.ToInt32(audioFile?.CurrentTime.TotalMilliseconds);

                if (CurrentSongCurrentTimeInSeconds == CurrentSongMaxTimeTotalSeconds)
                {
                    if (IsPlaybackLooped)
                    {
                        PlaySong(CurrentSong.Path);
                    }
                    else if (IsPlaybackContinous)
                    {
                        PlayNextSong(CurrentSong.Path);
                    }
                }
            }
        }

        #endregion
    }
}