using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using HeartStopper;
using WindowsGame1;

namespace HeartStopper
{
    public class AudioManager
    {
        private Song notChasedSong;
        private Song chasedSong;
        private ContentManager cm;

        private SoundEffect baa;

        public AudioManager(ContentManager content) 
        {
            
            cm = content;
            baa = cm.Load<SoundEffect>("Audio\\sheep_baa");
            notChasedSong = cm.Load<Song>("Audio\\The Forest and the Trees");
            chasedSong = cm.Load<Song>("Audio\\Action");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(notChasedSong);
        }

        public void soundBaa()
        {
            SoundEffectInstance baaInstance = baa.CreateInstance();
            baaInstance.IsLooped = false;
            baaInstance.Play();
        }
        public void changeChased(){
            MediaPlayer.Play(chasedSong);   
        }

        public void changeNotChased()
        {
            MediaPlayer.Play(notChasedSong);
        }
    }
}
