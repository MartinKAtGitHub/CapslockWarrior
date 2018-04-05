using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
 
namespace PixelWizards.Shared.Utiltiies
{
 
    public class DynamicTimelineBinding : MonoBehaviour
    {
        public List<GameObject> trackList = new List<GameObject>();
        public PlayableDirector timeline;
        public TimelineAsset timelineAsset;
        public bool autoBindTracks = true;
 
        // Use this for initialization
        private void Start()
        {
            if (autoBindTracks)
            {
				// BindTimelineTracks();
				Test();
            }
        }
 
  /*      public void BindTimelineTracks()
        {
            Debug.Log("Binding Timeline Tracks!");

            timelineAsset = (TimelineAsset)timeline.playableAsset;

             // iterate through tracks and map the objects appropriately
            for( var i = 0; i < trackList.Count; i ++)
            {
                if( trackList[i] != null)
                {
                 	 var track = (TrackAsset)timelineAsset.outputs;//[i].sourceObject;
					 timeline.SetGenericBinding(track, trackList[i]);
                }
            }
        }
*/
        public void Test()
        {
        	int i = 0;
			// This works it puts trackList[0](Gameobject) inn all the spots
			foreach (var output in timeline.playableAsset.outputs) 
			{
				i++;	
				timeline.SetGenericBinding(output.sourceObject, trackList[i-1]);
				// goes out of range
			}
        }

    }
}