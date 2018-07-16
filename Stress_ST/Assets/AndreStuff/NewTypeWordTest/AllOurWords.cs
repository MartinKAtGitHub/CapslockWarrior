using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AllOurWords {
	//Improvements. Make A List Of Refrences To Scripts Containing Info, Then Its Simply A Refrence Call And Logic Is Found
	//Make Class That Is Once Sentence Leader Word, Like Adjectives_Common -> "different". Said Class Know What To look For, So Only An Index Should Be Needed

	static List<string> TheSentence = new List<string> ();
	static List<string> SavedSentence = new List<string>();
	static int saverForMultiWords = 0;
	static int _TheSentence = 0;

	public static List<string> Sentences() {
		TheSentence.Clear();

		_TheSentence = Random.Range(0, 12);

		if (_TheSentence == 0) {

			TheSentence.Add(Adjectives_Common[Random.Range(0, Adjectives_Common.Length)]);

			_TheSentence = Random.Range(0, 3);

			if (_TheSentence == 0)
				TheSentence.Add(Noun_Proper[Random.Range(0, Noun_Proper.Length)]);
			else if (_TheSentence == 1)
				TheSentence.Add(Noun_Common[Random.Range(0, Noun_Common.Length)]);
			else
				TheSentence.Add(Noun_Countable[Random.Range(0, Noun_Countable.Length)]);

		} else if (_TheSentence == 1) {

			TheSentence.Add(Adjectives_Appearance[Random.Range(0, Adjectives_Appearance.Length)]);

			_TheSentence = Random.Range(0, 6);

			if (_TheSentence == 0)
				TheSentence.Add(Adjectives_Color[Random.Range(0, Adjectives_Color.Length)]);
			else if (_TheSentence == 1)
				TheSentence.Add(Adverb_Manner[Random.Range(0, Adverb_Manner.Length)]);
			else if (_TheSentence == 2)
				TheSentence.Add(Noun_Proper[Random.Range(0, Noun_Proper.Length)]);
			else if (_TheSentence == 3)
				TheSentence.Add(Noun_Common[Random.Range(0, Noun_Common.Length)]);
			else if (_TheSentence == 4)
				TheSentence.Add(Noun_Uncountable[Random.Range(0, Noun_Uncountable.Length)]);
			else
				TheSentence.Add(Noun_Countable[Random.Range(0, Noun_Countable.Length)]);

		} else if (_TheSentence == 2) {

			TheSentence.Add(Adjectives_Color[Random.Range(0, Adjectives_Color.Length)]);

			_TheSentence = Random.Range(0, 5);

			if (_TheSentence == 0)
				TheSentence.Add(Adverb_Frequency[Random.Range(0, Adverb_Frequency.Length)]);
			else if (_TheSentence == 1)
				TheSentence.Add(Adverb_Time_HowLong[Random.Range(0, Adverb_Time_HowLong.Length)]);
			else if (_TheSentence == 2)
				TheSentence.Add(Adverb_Time_When[Random.Range(0, Adverb_Time_When.Length)]);
			else if (_TheSentence == 3)
				TheSentence.Add(Noun_Common[Random.Range(0, Noun_Common.Length)]);
			else 
				TheSentence.Add(Noun_Countable[Random.Range(0, Noun_Countable.Length)]);

		} else if (_TheSentence == 3) {

			TheSentence.Add(Adjectives_Condition[Random.Range(0, Adjectives_Condition.Length)]);

			_TheSentence = Random.Range(0, 7);

			if (_TheSentence == 0)
				TheSentence.Add(Adverb_Frequency[Random.Range(0, Adverb_Frequency.Length)]);
			else if (_TheSentence == 1)
				TheSentence.Add(Adverb_Manner[Random.Range(0, Adverb_Manner.Length)]);
			else if (_TheSentence == 2)
				TheSentence.Add(Adverb_Place[Random.Range(0, Adverb_Place.Length)]);
			else if (_TheSentence == 3)
				TheSentence.Add(Adverb_Time_When[Random.Range(0, Adverb_Time_When.Length)]);
			else if (_TheSentence == 4)
				TheSentence.Add(Adverb_Time_HowLong[Random.Range(0, Adverb_Time_HowLong.Length)]);
			else if (_TheSentence == 5)
				TheSentence.Add(Noun_Common[Random.Range(0, Noun_Common.Length)]);
			else
				TheSentence.Add(Noun_Proper[Random.Range(0, Noun_Proper.Length)]);

		} else if (_TheSentence == 4) {

			TheSentence.Add(Adjectives_Negative_Personality[Random.Range(0, Adjectives_Negative_Personality.Length)]);

			_TheSentence = Random.Range(0, 10);

			if (_TheSentence == 0)
				TheSentence.Add(Noun_Proper[Random.Range(0, Noun_Proper.Length)]);
			else if (_TheSentence == 1)
				TheSentence.Add(Adjectives_Color[Random.Range(0, Adjectives_Color.Length)]);
			else if (_TheSentence == 2)
				TheSentence.Add(Adverb_Frequency[Random.Range(0, Adverb_Frequency.Length)]);
			else if (_TheSentence == 3)
				TheSentence.Add(Adverb_Place[Random.Range(0, Adverb_Place.Length)]);
			else if (_TheSentence == 4)
				TheSentence.Add(Adverb_Time_When[Random.Range(0, Adverb_Time_When.Length)]);
			else if (_TheSentence == 5)
				TheSentence.Add(Adverb_Time_HowLong[Random.Range(0, Adverb_Time_HowLong.Length)]);
			else if (_TheSentence == 6)
				TheSentence.Add(Adverb_Degree[Random.Range(0, Adverb_Degree.Length)]);
			else if (_TheSentence == 7)
				TheSentence.Add(Noun_Common[Random.Range(0, Noun_Common.Length)]);
			else if (_TheSentence == 8)
				TheSentence.Add(Noun_Countable[Random.Range(0, Noun_Countable.Length)]);
			else
				TheSentence.Add(Noun_Uncountable[Random.Range(0, Noun_Uncountable.Length)]);

		} else if (_TheSentence == 5) {

			TheSentence.Add(Adjectives_Positive_Personality[Random.Range(0, Adjectives_Positive_Personality.Length)]);

			_TheSentence = Random.Range(0, 6);

			if (_TheSentence == 0)
				TheSentence.Add(Adverb_Place[Random.Range(0, Adverb_Place.Length)]);
			else if (_TheSentence == 1)
				TheSentence.Add(Adverb_Time_When[Random.Range(0, Adverb_Time_When.Length)]);
			else if (_TheSentence == 2)
				TheSentence.Add(Adverb_Time_HowLong[Random.Range(0, Adverb_Time_HowLong.Length)]);
			else if (_TheSentence == 3)
				TheSentence.Add(Noun_Common[Random.Range(0, Noun_Common.Length)]);
			else if (_TheSentence == 4)
				TheSentence.Add(Adverb_Frequency[Random.Range(0, Adverb_Frequency.Length)]);
			else
				TheSentence.Add(Noun_Proper[Random.Range(0, Noun_Proper.Length)]);

		} else if (_TheSentence == 6) {

			TheSentence.Add(Adjectives_Negative_Personality[Random.Range(0, Adjectives_Negative_Personality.Length)]);

			_TheSentence = Random.Range(0, 5);

			if (_TheSentence == 0)
				TheSentence.Add(Adverb_Frequency[Random.Range(0, Adverb_Frequency.Length)]);
			else if (_TheSentence == 1)
				TheSentence.Add(Noun_Proper[Random.Range(0, Noun_Proper.Length)]);
			else if (_TheSentence == 2)
				TheSentence.Add(Adverb_Time_When[Random.Range(0, Adverb_Time_When.Length)]);
			else if (_TheSentence == 3)
				TheSentence.Add(Adverb_Time_HowLong[Random.Range(0, Adverb_Time_HowLong.Length)]);
			else 
				TheSentence.Add(Noun_Common[Random.Range(0, Noun_Common.Length)]);
		

		} else if (_TheSentence == 7) {

			TheSentence.Add(Adverb_Frequency[Random.Range(0, Adverb_Frequency.Length)]);

			_TheSentence = Random.Range(0, 10);

			if (_TheSentence == 0)
				TheSentence.Add(Adjectives_Common[Random.Range(0, Adjectives_Common.Length)]);
			else if (_TheSentence == 1)
				TheSentence.Add(Adjectives_Appearance[Random.Range(0, Adjectives_Appearance.Length)]);
			else if (_TheSentence == 2)
				TheSentence.Add(Adjectives_Color[Random.Range(0, Adjectives_Color.Length)]);
			else if (_TheSentence == 3)
				TheSentence.Add(Adjectives_Condition[Random.Range(0, Adjectives_Condition.Length)]);
			else if (_TheSentence == 4)
				TheSentence.Add(Adjectives_Positive_Personality[Random.Range(0, Adjectives_Positive_Personality.Length)]);
			else if (_TheSentence == 5)
				TheSentence.Add(Adjectives_Negative_Personality[Random.Range(0, Adjectives_Negative_Personality.Length)]);
			else if (_TheSentence == 6)
				TheSentence.Add(Adverb_Place[Random.Range(0, Adverb_Place.Length)]);
			else if (_TheSentence == 7)
				TheSentence.Add(Adverb_Time_When[Random.Range(0, Adverb_Time_When.Length)]);
			else if (_TheSentence == 8)
				TheSentence.Add(Verbs_Action_Transitive[Random.Range(0, Verbs_Action_Transitive.Length)]);
			else {
				TheSentence.Add(Noun_Proper[Random.Range(0, Noun_Proper.Length)]);
				TheSentence.Add(Verbs_Stative[Random.Range(0, Verbs_Stative.Length)]);//Just A Test
			}

		} else if (_TheSentence == 8) {

			TheSentence.Add(Adverb_Manner[Random.Range(0, Adverb_Manner.Length)]);

			_TheSentence = Random.Range(0, 9);

			if (_TheSentence == 0)
				TheSentence.Add(Adjectives_Appearance[Random.Range(0, Adjectives_Appearance.Length)]);
			else if (_TheSentence == 1)
				TheSentence.Add(Adjectives_Color[Random.Range(0, Adjectives_Color.Length)]);
			else if (_TheSentence == 2)
				TheSentence.Add(Adjectives_Condition[Random.Range(0, Adjectives_Condition.Length)]);
			else if (_TheSentence == 3)
				TheSentence.Add(Adjectives_Positive_Personality[Random.Range(0, Adjectives_Positive_Personality.Length)]);
			else if (_TheSentence == 4)
				TheSentence.Add(Adjectives_Negative_Personality[Random.Range(0, Adjectives_Negative_Personality.Length)]);
			else if (_TheSentence == 5)
				TheSentence.Add(Adverb_Time_When[Random.Range(0, Adverb_Time_When.Length)]);
			else if (_TheSentence == 6)
				TheSentence.Add(Adverb_Time_HowLong[Random.Range(0, Adverb_Time_HowLong.Length)]);
			else if (_TheSentence == 7)
				TheSentence.Add(Verbs_Action_Transitive[Random.Range(0, Verbs_Action_Transitive.Length)]);
			else 
				TheSentence.Add(Verbs_Stative[Random.Range(0, Verbs_Stative.Length)]);

		} else if (_TheSentence == 9) {

			TheSentence.Add(Adverb_Time_HowOften[Random.Range(0, Adverb_Time_HowOften.Length)]);

			_TheSentence = Random.Range(0, 14);

			if (_TheSentence == 0)
				TheSentence.Add(Adjectives_Common[Random.Range(0, Adjectives_Common.Length)]);
			else if (_TheSentence == 1)
				TheSentence.Add(Adjectives_Appearance[Random.Range(0, Adjectives_Appearance.Length)]);
			else if (_TheSentence == 2)
				TheSentence.Add(Adjectives_Color[Random.Range(0, Adjectives_Color.Length)]);
			else if (_TheSentence == 3)
				TheSentence.Add(Adverb_Time_HowOften[Random.Range(0, Adverb_Time_HowOften.Length)]);
			else if (_TheSentence == 4)
				TheSentence.Add(Adjectives_Condition[Random.Range(0, Adjectives_Condition.Length)]);
			else if (_TheSentence == 5)
				TheSentence.Add(Adjectives_Positive_Personality[Random.Range(0, Adjectives_Positive_Personality.Length)]);
			else if (_TheSentence == 6)
				TheSentence.Add(Adjectives_Negative_Personality[Random.Range(0, Adjectives_Negative_Personality.Length)]);
			else if (_TheSentence == 7)
				TheSentence.Add(Adverb_Manner[Random.Range(0, Adverb_Manner.Length)]);
			else if (_TheSentence == 8)
				TheSentence.Add(Adverb_Place[Random.Range(0, Adverb_Place.Length)]);
			else if (_TheSentence == 9)
				TheSentence.Add(Adverb_Time_When[Random.Range(0, Adverb_Time_When.Length)]);
			else if (_TheSentence == 10)
				TheSentence.Add(Adverb_Time_HowLong[Random.Range(0, Adverb_Time_HowLong.Length)]);
			else if (_TheSentence == 11)
				TheSentence.Add(Verbs_Action_Transitive[Random.Range(0, Verbs_Action_Transitive.Length)]);
			else if (_TheSentence == 12)
				TheSentence.Add(Verbs_Stative[Random.Range(0, Verbs_Stative.Length)]);
			else
				TheSentence.Add(Adverb_Place[Random.Range(0, Adverb_Place.Length)]);

		} else if (_TheSentence == 10) {

			TheSentence.Add(Adverb_Degree[Random.Range(0, Adverb_Degree.Length)]);

			_TheSentence = Random.Range(0, 13);

			if (_TheSentence == 0)
				TheSentence.Add(Adjectives_Common[Random.Range(0, Adjectives_Common.Length)]);
			else if (_TheSentence == 1)
				TheSentence.Add(Adjectives_Appearance[Random.Range(0, Adjectives_Appearance.Length)]);
			else if (_TheSentence == 2)
				TheSentence.Add(Adjectives_Color[Random.Range(0, Adjectives_Color.Length)]);
			else if (_TheSentence == 3)
				TheSentence.Add(Adjectives_Condition[Random.Range(0, Adjectives_Condition.Length)]);
			else if (_TheSentence == 4)
				TheSentence.Add(Adjectives_Positive_Personality[Random.Range(0, Adjectives_Positive_Personality.Length)]);
			else if (_TheSentence == 5)
				TheSentence.Add(Adjectives_Negative_Personality[Random.Range(0, Adjectives_Negative_Personality.Length)]);
			else if (_TheSentence == 6)
				TheSentence.Add(Adverb_Manner[Random.Range(0, Adverb_Manner.Length)]);
			else if (_TheSentence == 7)
				TheSentence.Add(Adverb_Place[Random.Range(0, Adverb_Place.Length)]);
			else if (_TheSentence == 8)
				TheSentence.Add(Adverb_Time_When[Random.Range(0, Adverb_Time_When.Length)]);
			else if (_TheSentence == 9)
				TheSentence.Add(Adverb_Time_HowLong[Random.Range(0, Adverb_Time_HowLong.Length)]);
			else if (_TheSentence == 10)
				TheSentence.Add(Adverb_Time_HowOften[Random.Range(0, Adverb_Time_HowOften.Length)]);
			else if (_TheSentence == 11)
				TheSentence.Add(Verbs_Stative[Random.Range(0, Verbs_Stative.Length)]);
			else
				TheSentence.Add(Noun_Proper[Random.Range(0, Noun_Proper.Length)]);

		} else {

			TheSentence.Add(Verbs_Action_Transitive[Random.Range(0, Verbs_Action_Transitive.Length)]);

			_TheSentence = Random.Range(0, 5);

			if (_TheSentence == 0)
				TheSentence.Add(Noun_Proper[Random.Range(0, Noun_Proper.Length)]);
			else if (_TheSentence == 1)
				TheSentence.Add(Adverb_Time_When[Random.Range(0, Adverb_Time_When.Length)]);
			else if (_TheSentence == 2)
				TheSentence.Add(Adverb_Time_HowLong[Random.Range(0, Adverb_Time_HowLong.Length)]);
			else if (_TheSentence == 3) {
				TheSentence.Add("the");
				TheSentence.Add(Noun_Common[Random.Range(0, Noun_Common.Length)]);
			} else { 
				TheSentence.Add("the");
				TheSentence.Add(Noun_Countable[Random.Range(0, Noun_Countable.Length)]);
			} 
			
 		}


		SavedSentence.Clear();
		foreach (string s in TheSentence) {//"Copying" Words From List 'A' To List 'B'
			SavedSentence.Add(s);
		}
		TheSentence.Clear();

		foreach (string s in SavedSentence) {
			saverForMultiWords = 0;
			for (int i = 0; i < s.Length; i++) {
				if (s[i] == ' ') {//Splitting Sentence "Olaf The Great" -> "Olaf" "The" "Great"
					TheSentence.Add(s.Substring(saverForMultiWords, i - saverForMultiWords));
					saverForMultiWords = i + 1;
				} else if (i + 1 == s.Length) {//At The Last Element
					TheSentence.Add(s.Substring(saverForMultiWords, i - saverForMultiWords + 1));
				}
			}

		}

/*		string a = ""; //To Get The Sentence Out
		foreach (string s in TheSentence) {
			a += s + " ";
		}

		Debug.Log(a);*/
		return TheSentence;

	}


	//www.gingersoftware.com/content/grammar-rules/adjectives/lists-of-adjectives/
	static string[] Adjectives_Common = new string[] {//Common Adjectives				Can Be Devided Into: Sizes Like -> Few, Many.   Time Like -> early
		"good",
		"new",
		"first",
		"last",
		"long",
		"great",
		"little",
		"own",
		"other",
		"old",
		"right",
		"big",
		"high",
		"different",
		"small",
		"large",
		"next",
		"early",
		"young",
		"important",
		"public",
		"bad",
		"same",
		"able",
	};

	static string[] Adjectives_Appearance = new string[] {//Appearance Adjectives
		"adorable",
		"beautiful",
		"clean",
		"drab",
		"elegant",
		"fancy",
		"glamorous",
		"handsome",
		"long",
		"magnificent",
		"old-fashioned",
		"plain",
		"quaint",
		"sparkling",
		"ugliest",
		"unsightly",
		"wide-eyed",
	};

	static string[] Adjectives_Color = new string[] {//Color Adjectives
		"red",
		"orange",
		"yellow",
		"green",
		"blue",
		"purple",
		"gray",
		"black",
		"white",
	};

	static string[] Adjectives_Condition = new string[] {//Condition Adjectives
		"alive",
		"better",
		"careful",
		"clever",
		"dead",
		"easy",
		"famous",
		"gifted",
		"helpful",
	};

	static string[] Adjectives_Positive_Personality = new string[] {//Positive Personality Adjectives
		"agreeable",
		"brave",
		"calm",
		"delightful",
		"eager",
		"faithful",
		"gentle",
		"happy",
		"jolly",
	};

	static string[] Adjectives_Negative_Personality = new string[] {//Negative Personality Adjectives
		"angry",
		"bewildered",
		"clumsy",
		"defeated",
		"embarrassed",
		"fierce",
		"grumpy",
		"helpless",
		"itchy",
	};

	static string[] Adverb_Frequency = new string[] {//Frequency Adverbs
		"always",
		"annually",
		"constantly",
		"daily",
		"eventually",
		"ever",
		"frequently",
		"generally",
		"hourly",
		"infrequently",
		"later",
		"monthly",
		"never",
		"next",
		"nightly",
	};

	static string[] Adverb_Manner = new string[] {//Manner Adverbs
		"accidentally",
		"angrily",
		"anxiously",
		"awkwardly",
		"badly",
		"beautifully",
		"boldly",
		"bravely",
		"bightly",
		"busily",
		"calmly",
		"carefully",
		"cautiously",
		"cheerfully",
		"clearly",
	};

	static string[] Adverb_Place = new string[] {//Place Adverbs
		"abroad",
		"across",
		"ahead",
		"back",
		"backwards",
		"beyond",
		"down",
		"downwards",
		"eastwards",
		"everywhere",
		"here",
		"in",
		"indoors",
		"inside",
		"outside",
		"overseas",
		"there",
		"west",
		"yonder",
	};

	static string[] Adverb_Time_When = new string[] {//When Time Adverbs
		"yesterday",
		"today",
		"tomorrow",
		"later",
		"last year",
		"now",
	};

	static string[] Adverb_Time_HowLong = new string[] {//HowLong Time Adverbs
		"all morning",
		"for hours",
		"since last week",
	};

	static string[] Adverb_Time_HowOften = new string[] {//HowOften Time Adverbs
		"frequently",
		"never",
		"sometimes",
		"often",
		"anually",
	};

	static string[] Adverb_Degree= new string[] {//Degree Adverbs
		"almost",
		"absolutely",
		"barely",
		"completely",
		"deeply",
		"enough",
		"enormously",
		"extremely",
		"fairly",
		"fully",
		"greatly",
		"hardly",
		"incredibly",
		"practically",
		"quite",
		"scarcely",
		"somewhat",
		"terribly",
		"virtually",
	};


	static string[] Verbs_Action_Transitive = new string[] {//Transitive Action Verbs  (this is an action that a person can do, but requires something after to complete the sentence (direct object))
		"bring",
		"send",
		"owe",
		"contain",
		"buy",
		"show",
		"take",
		"tell",
		"verify",
		"check",
		"get",
		"wash",
		"finalize",
		"annoy",
		"lay",
		"lend",
		"offer",
		"edit",
		"make",
		"phone",
	};

	static string[] Verbs_Stative = new string[] {//Stative Verbs (Verbs Related To Thoughts, Emotions, Relationships. Senses, States Of Being And Measurements). (And To Corretcly Used Should Be Devided Into Those. 'Paul FEELS Rotten Today', Feels Is A Stense, Cannot Be Change With Emotions Like 'Paul Loves Rotten Today'
		"agree",
		"appear",
		"believe",
		"belong",
		"concern",
		"consist",
		"contain",
		"depend",
		"deserve",
		"disagree",
		"dislike",
		"doubt",
		"feel",
		"fit",
		"hate",
		"hear",
		"imagine",
		"impress",
		"include",
		"involve",
	};

	//nouns ->  identifies a: 
	//person -> (A term for a person, whether proper name, gender, title, or class, is a noun)
	//animal -> (A term for an animal, whether proper name, species, gender, or class is a noun)
	//place -> (A term for a place, whether proper name, physical location, or general locale is a noun)
	//thing -> (A term for a thing, whether it exists now, will exist, or existed in the past is a noun)
	//idea -> (A term for an idea, be it a real, workable idea or a fantasy that might never come to fruition is a noun)

	static string[] Noun_Proper = new string[] {//Proper Noun.  Always Have A Capital Letter, And Are 'one-of-a-kind' Items. (specific people, places or things)
		"Agatha Christie",
		"Cleopatra",
		"Oreos",
		"San Francisco",
		"Mr. Bell",
		"Jupiter",
		"Sara",
		"The Library of Congress",
		"Walt Disney",
		"Mount Kilimanjaro",
		"Minnesota",
		"Australia",
		"Fluffy",
		"Google",
		"Microsoft",
		"Asus",

	};

	static string[] Noun_Common = new string[] {//Common Noun. (Non-specific people, places or things)
		"dentist",
		"fish",
		"toothbrush",
		"school",
		"church",
		"store",
		"teacher",
		"student",
		"cat",
		"people",
		"way",
		"day",
		"child",
		"family",
		"hand",
		"company",
		"government",
		"night",
		"military",

	};


	static string[] Noun_Uncountable = new string[] {//Uncountable Noun.  (liquids, abstact ideas (advice, chaos, motivation), powder and grain, mass nouns (furniture, hair, transportation), natural phenomena (sunshine, snow, rain), states of being (sleep, stress, childhood), feelings, gass
		"milk",
		"water",
		"advice",
		"chaos",
		"motivation",
		"rice",
		"wheat",
		"sand",
		"furniture",
		"hair",
		"transportation",
		"sunshine",
		"snow",
		"rain",
		"weather",
		"sleep",
		"stress",
		"childhood",
		"anger",
		"oxygen",

	};

	static string[] Noun_Countable = new string[] {//Countable Noun.  Items You Can Count  
		"bed",
		"cat",
		"movie",
		"train",
		"country",
		"book",
		"phone",
		"match",
		"speaker",
		"clock",
		"pen",
		"violin",
		"light bulbs",
		"bell",
		"animal",
		"answer",
		"bottle",
		"bus",
		"bush",
	};


}









#region old

/*	placement = Random.Range(0,CorrelativeConjunctions.Length / 2) * 2;

Debug.Log ("The " + TheObjects [Random.Range (0, TheObjects.Length)] + " " + RelativePronoun [Random.Range (0, RelativePronoun.Length)] + " " + PersonName [Random.Range (0, PersonName.Length)] + " " + Adjective [Random.Range (0, Adjective.Length)] + " " + TheObjects [Random.Range (0, TheObjects.Length)] + " " + Verbs [Random.Range (0, Verbs.Length)] 
	+ " " + SubordinatingConjunctions [Random.Range (0, SubordinatingConjunctions.Length)] + " the " + Adjective[Random.Range (0, Adjective.Length)] + " " + TheObjects [Random.Range (0, TheObjects.Length)] + " " + Verbs [Random.Range (0, Verbs.Length)] );
Debug.Log ("The " + TheObjects [Random.Range (0, TheObjects.Length)] + " " + RelativePronoun [Random.Range (0, RelativePronoun.Length)] + " " + PersonName [Random.Range (0, PersonName.Length)] 
	+ " " + Verbs2 [Random.Range (0, Verbs2.Length)] + ", " + CorrelativeConjunctions [placement] + " " + Verbs [Random.Range (0, Verbs.Length)] + " " + CorrelativeConjunctions [placement + 1] + " " + Verbs [Random.Range (0, Verbs.Length)]);



placement = Random.Range(0,CorrelativeConjunctions.Length / 2) * 2;
Debug.Log ("The " + TheObjects [Random.Range (0, TheObjects.Length)] + " " + RelativePronoun [Random.Range (0, RelativePronoun.Length)] + " " + Adjective [Random.Range (0, Adjective.Length)]  + " " + PersonName [Random.Range (0, PersonName.Length)] + " " + CorrelativeConjunctions[placement]
	+ " " + AdjectivePast [Random.Range (0, AdjectivePast.Length)]  + " " + CorrelativeConjunctions[placement+1] + " " + AdjectivePast [Random.Range (0, AdjectivePast.Length)] + " " + Verbs2 [Random.Range (0, Verbs2.Length)] + " " + PersonName [Random.Range (0, PersonName.Length)]);

Debug.Log (PersonName [Random.Range (0, PersonName.Length)] + " " + Verbs [Random.Range (0, Verbs.Length)] + " the " + TheObjects [Random.Range (0, TheObjects.Length)] + " " + RelativePronoun [Random.Range (0, RelativePronoun.Length)] + " " + PersonName [Random.Range (0, PersonName.Length)] + " " + AdjectivePast [Random.Range (0, AdjectivePast.Length)]);
Debug.Log ("the " + TheObjects [Random.Range (0, TheObjects.Length)] + " " + RelativePronoun [Random.Range (0, RelativePronoun.Length)] + " " + PersonName [Random.Range (0, PersonName.Length)] + " " + AdjectivePast [Random.Range (0, AdjectivePast.Length)] + " " + Verbs [Random.Range (0, Verbs.Length)] + " " + PersonName [Random.Range (0, PersonName.Length)] 
	+ " " + SubordinatingConjunctions [Random.Range (0, SubordinatingConjunctions.Length)] + " " + PersonName [Random.Range (0, PersonName.Length)] + " " + Verbs [Random.Range (0, Verbs.Length)] + " the " + TheObjects [Random.Range (0, TheObjects.Length)]);
Debug.Log ("The " + Adjective [Random.Range (0, Adjective.Length)] + " " + TheObjects [Random.Range (0, TheObjects.Length)] + " " + Verbs [Random.Range (0, Verbs.Length)] + " " + SubordinatingConjunctions [Random.Range (0, SubordinatingConjunctions.Length)] + " the " + TheObjects [Random.Range (0, TheObjects.Length)] + " " + Verbs [Random.Range (0, Verbs.Length)] + " " + PersonName [Random.Range (0, PersonName.Length)]);


Debug.Log ("the " + TheObjects [Random.Range (0, TheObjects.Length)] + " " + RelativePronoun [Random.Range (0, RelativePronoun.Length)] + " the " + TheObjects [Random.Range (0, TheObjects.Length)] + " " + AdjectivePast [Random.Range (0, AdjectivePast.Length)] + " " + Verbs2 [Random.Range (0, Verbs2.Length)] + " " + PersonName [Random.Range (0, PersonName.Length)]);
*/


/*	TheSentence.Add ("The ");
	TheSentence.Add (AdjectiveLiving [Random.Range (0, AdjectiveLiving.Length)]);
	TheSentence.Add (LivingSubjectVerbPlacementNoun [0]);
	TheSentence.Add (AdverbLiving [Random.Range (0, AdverbLiving.Length)]);
	TheSentence.Add (LivingSubjectVerbPlacementNoun [1]);*/

//Have To Seperate Between Movement Verbs. Ran, Swam, Jogged -> down the street. all of them can have a location added to them. he swam in water, he ran in water, he jogged in water. all of them are possible.
//So Movement -> Ran Swam Jogged
//Interaction -> stole, 
//human interaction -> spoke, beat, 
//Senses -> look, smell taste, sound, feel, see

//Verbs, gathering something or exhaling something are verbs that fit together.  the bee gathers honey.  the bee gathers corpses.  everything fits
//verbs with human motion like running or swimming fit together.				 only placement with distance.    the bee ran a marathon, ran a mile. ran to paris, ran to the big ben.....



/*public static class AllOurWordss {

	static List<string> TheSentence = new List<string>();
	static string saver = "";
	static string sas = "";


	static string CheckLetter = "";

	static void WordAdding_Ing(string words) {

		CheckLetter = words.Substring(words.Length - 1, 1);

		if (CheckLetter == "e") {
			TheSentence.Add(words.Substring(0, words.Length - 1) + "ing");
		} else {
			CheckLetter = words.Substring(words.Length - 2, 1);
			if (CheckLetter == "a" || CheckLetter == "e" || CheckLetter == "i" || CheckLetter == "o" || CheckLetter == "u") {//True If There Is A Vowel, Which Makes The Next Letter Become Duplicated. Spit-Spitting
				CheckLetter = words.Substring(words.Length - 1, 1);
				if (CheckLetter == "k" || CheckLetter == "r" || CheckLetter == "y" || CheckLetter == "m") {//Checking The Last Letter After The Few Instances Where The Letter Are'nt Duplicated
					TheSentence.Add(words + "ing");
				} else {
					TheSentence.Add(words + (words.Substring(words.Length - 1, 1) + "ing"));//Duplicating The Last Letter Pluss Adding Ing
				}
			} else {
				TheSentence.Add(words + "ing");
			}
		}
	}

	static void WordAdding_S(string words) {

		CheckLetter = words.Substring(words.Length - 1, 1);

		if (CheckLetter != "s") {//If S Isnt The Last Letter
			if (CheckLetter == "y") {//Check If Its Y. If True Change Y To i
				TheSentence.Add(words.Substring(0, words.Length - 1) + "ies");
			} else {
				TheSentence.Add(words + "s");
			}
		} else {//If False. one cactus, several cactie
			TheSentence.Add(words);
		}

	}



	static void FillAdverbSubject(string adverb, string subjectLiving) {
		string Test = "";
		bool PluralRng = false;
		int wordsToPlaceBefore = Random.Range(0, 2);
		int wordsToPlaceMiddle = 0;
		int wordsToPlaceAfter = 0;

		if (wordsToPlaceBefore == 1) {
			int BeforeAdverb = Random.Range(0, 2);

			if (BeforeAdverb == 0) {
				CheckLetter = adverb.Substring(0, 1).ToLower();
				if (CheckLetter == "a" || CheckLetter == "e" || CheckLetter == "i" || CheckLetter == "o" || CheckLetter == "u") {//True If There Is A Vowel, Which Makes The Next Letter Become Duplicated. Spit-Spitting
					TheSentence.Add("An");
				} else {
					TheSentence.Add("A");
				}
			} else if (BeforeAdverb == 1) {
				TheSentence.Add("A");
			}
		}

		TheSentence.Add(adverb);
		TheSentence.Add(AdjectiveLiving[Random.Range(0, AdjectiveLiving.Length)]);//Verb Adding 'ing'
		TheSentence.Add(subjectLiving);

	}

	static void FillSubjectVerb() {
		int PastPresentOrFuture = Random.Range(0, 3);

		TheSentence.Add("The");

		if (PastPresentOrFuture == 0) {//Past
			TheSentence.Add(subject1Living[Random.Range(0, subject1Living.Length)]);
			TheSentence.Add(verb1LivingPast[Random.Range(0, verb1LivingPast.Length)]);
		} else if (PastPresentOrFuture == 1) {//Present
			TheSentence.Add(subject1Living[Random.Range(0, subject1Living.Length)]);
			TheSentence.Add("is");
			TheSentence.Add(verb1LivingPresent[Random.Range(0, verb1LivingPresent.Length)]);
		} else {//Future
			TheSentence.Add(subject1Living[Random.Range(0, subject1Living.Length)]);
			TheSentence.Add("will");
			TheSentence.Add(verb1LivingFuture[Random.Range(0, verb1LivingFuture.Length)]);
		}








		
		return;




				//	string Test = "";
				//bool PluralRng = false;
				//int wordsToPlaceBefore = Random.Range(0, 3);
				//int wordsToPlaceMiddle = Random.Range(0, 2);
				//int wordsToPlaceAfter = 0;

				//if (wordsToPlaceBefore == 1) {
				//	int TheOrA = Random.Range(0, 3);

				//	if (TheOrA == 0) {//Add A/An 
				//		CheckLetter = subject.Substring(0, 1).ToLower();
				//		if (CheckLetter == "a" || CheckLetter == "e" || CheckLetter == "i" || CheckLetter == "o" || CheckLetter == "u") {//True If There Is A Vowel, Which Makes The Next Letter Become Duplicated. Spit-Spitting
				//			TheSentence.Add("An");
				//		} else {
				//			TheSentence.Add("A");
				//		}

				//	} else if (TheOrA == 1) {
				//		TheSentence.Add("The");

				//	} else {
				//		TheSentence.Add(Pronomen[Random.Range(0, Pronomen.Length)]);
				//	}

				//} else if (wordsToPlaceBefore == 2) {
				//	int TimedAdverbOrAdjective = Random.Range(0, 1);

				//	if (TimedAdverbOrAdjective == 0) {//Add Adverb Of Time
				//		CheckLetter = AdverbPast[Random.Range(0, AdverbPast.Length)];//Borrowing This Variable :D
				//		if (CheckLetter.Length > 1)
				//			TheSentence.Add(CheckLetter.Substring(0, 1) + CheckLetter.Substring(1, CheckLetter.Length - 1));
				//		else {
				//			TheSentence.Add(CheckLetter.ToUpper());
				//		}
				//	} else {
				//		TheSentence.Add(AdjectivePast[Random.Range(0, AdjectivePast.Length)]);

				//	}

				//	int TheOrA = Random.Range(0, 1);

				//	if (TheOrA == 0) {//Add A/An 
				//		CheckLetter = subject.Substring(0, 1).ToLower();
				//		if (CheckLetter == "a" || CheckLetter == "e" || CheckLetter == "i" || CheckLetter == "o" || CheckLetter == "u") {//True If There Is A Vowel, Which Makes The Next Letter Become Duplicated. Spit-Spitting
				//			TheSentence.Add("an");
				//		} else {
				//			TheSentence.Add("a");
				//		}

				//	} else {
				//		TheSentence.Add("the");

				//	}

				//}

				//TheSentence.Add(subject);
				//TheSentence.Add(verb1LivingPast[Random.Range(0, verb1LivingPast.Length)]);


	}



	//TODO Adverb Clauses, Independent Clauses
	
		//the owl flew. (how? the owl flew): Quietly, Fast, Loudly, Happily, Peacefully, Graciously, Slowly, Weirdly, low, high, nicely,
	 
		//Adverbs -> How?, When?, Where?, To What Extent?, Why?(Can Be An Adverb Clause).

		//If you need to use more than one adverb of time in a sentence, use them in this order:
		//1: how long 2: how often 3: when

		////When, Are Usualy Place At The End Of A Centence
		////He Saw Me TODAY.      He Is Leaving NOW.      It Came Out LAST YEAR
		////**** Important Time -> TODAY, He Saw Me
		////**** Neutral Time -> He Saw Me TODAY
	
		//Points Of Time: (definite)
		//* now, then, today, tomorrow, tonight, yesterday
		 
		//frequency: (definite)
		//* annually, daily, fortnightly, hourly, monthly, nightly, quarterly, weekly, yearly, bimonthly, biyearly , biannually
		
		//frequency: (indefinite)
		//* always, constantly, ever, frequently, generally, never, normally, occasionally, often, rarely, regularly, seldom, sometimes, regularly, usually
		 
		//relationships in time: (indefinite)
		//* already, before, early, earlier, eventually, finally, first, formerly,just, last, late, lately, next, previously, recently, since, soon, still, yet

		//Auxiliary verb + adverb + other verb
		
		//I have never seen her before.
		//I am seldom late for work.


		//**When**
		//- The Owl Flew YESTERDAY
		//- Today The Fish Drowned
		//- The fish ALWAYS Drowned
		
		//*Adverb Past	- Before: today, yesterday, annually, daily, fortnightly, hourly, monthly, nightly, quarterly, weekly, yearly, bimonthly, biyearly , biannually, occasionally, sometimes, early, earlier, eventually, finally, first, formerly,last, late, lately, next, previously,
		//*Adverb Present - Before: now, then, today, annually, daily, fortnightly, hourly, monthly, nightly, quarterly, weekly, yearly, bimonthly, biyearly , biannually, constantly
		//*Adverb Future	- Before: now, then, today, yesterday 
		
		//*Adverb Past	- today, then, yesterday,constantly
		//*Adverb Present - now, 
		//*Adverb Future	- today, then, tomorrow, tonight,
		//*Adverb OnInAll	- *Time O'Clock*, annually, daily, fortnightly, hourly, monthly, nightly, quarterly, weekly, yearly, bimonthly, biyearly , biannually
		//*adverbs not used - always, constantly, ever, frequently



		//the owl flew. (when?yesterday, the owl flew): yesterday, today, now, never,  later, last year, all day, for a year, since 1996, for three days, for a week, for several years, since monday, since last century
		//											  often, never, always, seldom,rarely, monfhly, once a week, five days a week, seven times,
		//the owl flew. (when?last night, the owl flew): last night, at new year, at christmas, at easter, at 12 o'clock, 
		//the owl flew. (when? the owl flew, at noon): at noon, at dinner, at breakfast, at night, , Graciously, Slowly, Weirdly, low, high, nicely,


	 

	static string[] SubjectsTest = new string[] {

		"wheel",
		"olaf",
		"he",
		"they",
		"frog",
		"dinosaur",
		"i",


	};

	static string[] SubjectsTestOthers = new string[] {

		"the wheel",
		"olaf",
		"the frog",
		"the dinosaur",
		"frank",


	};


	static string[] SubjectTestWithThe = new string[] {

		"The wheel",
		"olaf",
		"he",
		"they",
		"the frog",
		"the dinosaur",
		"the teacher",
		"i",

	};

	static string[] verb1LivingPasttestFastSpeedy = new string[] {//past - I ran
		"ran",
		"shuffled",
		"stole",
		"skulked",
		"snuck",
		"slunk",
		"tiptoed",
		"arrived",
		"entered",
		"sprinted",
		"bolted",

	};
	static string[] verb1LivingPasttestSlowSpeedy = new string[] {//past - I ran
		"ran",
		"straggled",
		"strolled",
		"slogged",
		"trudged",
		"shuffled",
		"stole",
		"skulked",
		"snuck",
		"slunk",
		"tiptoed",
		"arrived",
		"entered",
		"sprinted",
		"bolted",

	};



	public static List<string> sentencetypeone() {



		TheSentence.Clear();

		#region testing

			//	TheSentence.Add (Adjective [Random.Range (0, Adjective.Length)]);
			//	TheSentence.Add (AdjectivePast [Random.Range (0, AdjectivePast.Length)]);
			//	TheSentence.Add (Adjective [Random.Range (0, Adjective.Length)]);
			//	return TheSentence;

			//TheSentence.Add ("The");
			//TheSentence.Add (Items [Random.Range (0, Items.Length)]);
			//TheSentence.Add (Items [Random.Range (0, Items.Length)]);
			//TheSentence.Add ("and");
			//TheSentence.Add (Items [Random.Range (0, Items.Length)]);
			//TheSentence.Add ("Corp");
			//return TheSentence;


			//TheSentence.Add (TTest1 [Random.Range (0, TTest1.Length)]);
			//TheSentence.Add (TTest2 [Random.Range (0, TTest2.Length)]);

			//saver = TTest3 [Random.Range (0, TTest3.Length)];

			//if (saver [0] == 'a' || saver [0] == 'e' || saver [0] == 'i' || saver [0] == 'o' || saver [0] == 'u' || saver [0] == 'i') {
			//	TheSentence.Add ("an");
			//} else {
			//	TheSentence.Add ("a");
			//}
			//TheSentence.Add (saver);

			//TheSentence.Add (TTest4 [Random.Range (0, TTest4.Length)]);





			//	TheSentence.Add ("The");
			//	TheSentence.Add (nonLivingObjects [Random.Range (0, nonLivingObjects.Length)]);
			//	TheSentence.Add (nonLivingBehaviour [Random.Range (0, nonLivingBehaviour.Length)]);
			//	TheSentence.Add (placements [Random.Range (0, placements.Length)]);
			//	TheSentence.Add ("the");
			//	TheSentence.Add (nonLivingObjects [Random.Range (0, nonLivingObjects.Length)]);
		



			//TheSentence.Add ("of");

			//TheSentence.Add (Adjective [Random.Range (0, Adjective.Length)]);
			//TheSentence.Add (describingss [Random.Range (0, describingss.Length)]);


			//TheSentence.Add (subject1Living [Random.Range (0, subject1Living.Length)]);
			//TheSentence.Add (verb1LivingPast [Random.Range (0, verb1LivingPast.Length)]);
			//TheSentence.Add (prepositionPlacement [Random.Range (0, prepositionPlacement.Length)]);
			//TheSentence.Add (nounPlacement [Random.Range (0, nounPlacement.Length)]);


			//TheSentence.Add (subject1Living [Random.Range (0, subject1Living.Length)]);
			//TheSentence.Add (prepositionPlacement [Random.Range (0, prepositionPlacement.Length)]);
			//TheSentence.Add (nounPlacement [Random.Range (0, nounPlacement.Length)]);
			//TheSentence.Add (verb1LivingPast [Random.Range (0, verb1LivingPast.Length)]);
			//TheSentence.Add (prepositionItem [Random.Range (0, prepositionItem.Length)]);
			//TheSentence.Add (subject1NonLiving [Random.Range (0, subject1NonLiving.Length)]);

		//		TheSentence.Add (RandomPhrase [Random.Range (0, RandomPhrase.Length)]);

		//	TheSentence.Add ("The");
		//	TheSentence.Add (AdverbLiving [Random.Range (0, AdverbLiving.Length)]);
		//	TheSentence.Add (AdjectiveLiving [Random.Range (0, AdjectiveLiving.Length)]);
		//	TheSentence.Add (subject2Living [Random.Range (0, subject2Living.Length)]);
		//	TheSentence.Add (VerbLivingt [Random.Range (0, VerbLivingt.Length)] + "s");
		//	TheSentence.Add (LivingSubjectVerbPlacementNoun [0]);

		//	TheSentence.Add (AdverbLiving [Random.Range (0, AdverbLiving.Length)]);
		//	TheSentence.Add (subject2Living [Random.Range (0, subject2Living.Length)]);
		//	TheSentence.Add (VerbLivingt [Random.Range (0, VerbLivingt.Length)] + "s");

		#endregion
		string CheckLetter;















		if (Random.Range(0, 0) == 0) {//Adverb - Verb'ing - subject's

			//		TheSentence.Add(SubjectTestWithThe[Random.Range(0, SubjectTestWithThe.Length)] + " " + InchoativeVerbsLiving[Random.Range(0, InchoativeVerbsLiving.Length)] + " ");//Any Subject With An Inchoative Verb Is OK
			TheSentence.Add(SubjectTestWithThe[Random.Range(0, SubjectTestWithThe.Length)] + " " + verb1LivingPast[Random.Range(0, verb1LivingPast.Length)] + " ");//Any Subject With An Inchoative Verb Is OK








			//--->>>>>	FillSubjectVerb();


			//	TheSentence.Add(AdverbLiving[Random.Range(0, AdverbLiving.Length)]);//Adverb Adding Nothing

			//	WordAdding_Ing(VerbLivingt[Random.Range(0, VerbLivingt.Length)]);//Verb Adding 'ing'

			//	WordAdding_S(subject2Living[Random.Range(0, subject2Living.Length)]);//Subject Adding 's' or 'ies

		} else if (Random.Range(0, 100) == 0) {//Adverb - Subject's

			TheSentence.Add(AdverbLiving[Random.Range(0, AdverbLiving.Length)]);//Adverb Adding Nothing

			TheSentence.Add(subject2Living[Random.Range(0, subject2Living.Length)]);//Subject Adding 's' or 'ies

		} else if (Random.Range(0, 100) == 0) {//Fill Double Word     


			FillAdverbSubject(AdverbLiving[Random.Range(0, AdverbLiving.Length)], subject2Living[Random.Range(0, subject2Living.Length)]);

		} else if (Random.Range(0, 100) == 0) {

			FillSubjectVerb();

		} else if (Random.Range(0, 0) == 0) {//Verb - Subject                     //Rnd To Add -Ing Or -S To The Different Words

			sas = verb1LivingPresent[Random.Range(0, verb1LivingPresent.Length)];
			if (Random.Range(0, 2) == 0) {//Chance To Add S
				CheckLetter = sas.Substring(sas.Length - 1, 1);
				if (CheckLetter == "h") {
					sas += "es";
				} else {
					if (CheckLetter != "s") {
						if (CheckLetter == "y") {
							CheckLetter = sas.Substring(sas.Length - 2, 1);
							if (CheckLetter == "a" || CheckLetter == "e" || CheckLetter == "i" || CheckLetter == "o" || CheckLetter == "u" || CheckLetter == "y") {//If True Then There Might Need To Be Added And Aditional Letter
								sas += "s";
							} else {
								sas = (sas.Substring(0, sas.Length - 1) + "ies");
							}
						} else {
							sas += "s";
						}
					}
				}
			}
			TheSentence.Add(sas);

			sas = subject2Living[Random.Range(0, subject2Living.Length)];
			CheckLetter = sas.Substring(sas.Length - 1, 1);
			if (CheckLetter != "s") {
				if (CheckLetter == "y") {
					sas = (sas.Substring(0, sas.Length - 1) + "ies");
				} else {
					sas += "s";
				}
			}
			TheSentence.Add(sas);


		} else if (Random.Range(0, 4) == 0) {//Rnd To Add -Ing Or -S To The Different Words
			sas = verb1LivingPresent[Random.Range(0, verb1LivingPresent.Length)];
			if (Random.Range(0, 2) == 0) {//Chance To Add Ing
				if (sas.Substring(sas.Length - 1, 1) == "e") {
					sas = (sas.Substring(0, sas.Length - 1) + "ing");
				} else {
					sas += "ing";
				}
			}
			TheSentence.Add(sas);


			sas = subject2Living[Random.Range(0, subject2Living.Length)];
			if (Random.Range(0, 2) == 0) {//Chance To Add S
				if (sas.Substring(sas.Length - 1, 1) != "s") {
					sas += "s";
				}
			}
			TheSentence.Add(sas);
		}

		string a = "";
		foreach (string s in TheSentence) {
			a += s + " ";
		}

		Debug.Log(a);
		return TheSentence;

		//if (Random.Range(0, 4) == 0) {// 

		//	TheSentence.Add("The");
		//	TheSentence.Add(AdverbLiving[Random.Range(0, AdverbLiving.Length)]);
		//	TheSentence.Add(AdjectiveLiving[Random.Range(0, AdjectiveLiving.Length)]);
		//	TheSentence.Add(subject2Living[Random.Range(0, subject2Living.Length)] + "s");

		//} else if (Random.Range(0, 4) == 0) {

		//	TheSentence.Add(subject1NonLiving[Random.Range(0, subject1NonLiving.Length)]);
		//	TheSentence.Add(verb1NonLivingPast[Random.Range(0, verb1NonLivingPast.Length)]);
		//	TheSentence.Add(prepositionPlacement[Random.Range(0, prepositionPlacement.Length)]);
		//	TheSentence.Add(nounPlacement[Random.Range(0, nounPlacement.Length)]);

		//} else if (Random.Range(0, 4) == 0) {//Adjective - Subject

		//	TheSentence.Add(AdjectiveLiving[Random.Range(0, AdjectiveLiving.Length)]);
		//	TheSentence.Add(subject2Living[Random.Range(0, subject2Living.Length)] + "s");

		//} else if (Random.Range(0, 4) == 0) {

		//	TheSentence.Add(RandomPhrase[Random.Range(0, RandomPhrase.Length)]);


		//} else {//Adverb - Adjective

		//	TheSentence.Add(AdverbLiving[Random.Range(0, AdverbLiving.Length)]);
		//	TheSentence.Add(AdjectiveLiving[Random.Range(0, AdjectiveLiving.Length)]);

		//}

		//return TheSentence;

		//	return "The " + TheObjects [Random.Range (0, TheObjects.Length)] + " " + RelativePronoun [Random.Range (0, RelativePronoun.Length)] + " " + PersonName [Random.Range (0, PersonName.Length)] 
		//			    + " " + Verbs2 [Random.Range (0, Verbs2.Length)] + ", " + CorrelativeConjunctions [placement] + " " + Verbs [Random.Range (0, Verbs.Length)] + " " + CorrelativeConjunctions [placement + 1] + " " + Verbs [Random.Range (0, Verbs.Length)];
	}


	static string[] AdverbPast = new string[] {
		"then",
		"today",
		"yesterday",
	};

	static string[] AdverbPresent = new string[] {
		"now",
		"then",
		"today",
	};
	static string[] AdverbFuture = new string[] {
		"then",
		"today",
		"tonight",
	};

	static string[] Pronomen = new string[] {
		"his",
		"her",
		"its",
		"my",
		"your",
		"their",
	};

	static string[] RandomPhrase = new string[] {
		"Close but no cigar",
		"Son of a gun",
		"Ring any bells",
		"Quality time",
		"Wouldn't harm a fly",
		"Break the ice",
		"Lickety split",
		"There's no i in team",
		"Ugly duckling",
		"Playing Possum",
		"Elvis has left the building",
		"Cry over spilt milk",
		"Back to the drawing board",
		"Right out of the gate",
		"Quick and dirty",
		"Jaws of death",
		"Off one's base",
		"Goody two-shoes",
		"Down and out",
		"A fool and his money are soon parted",
		"Mouth-watering",
		"Happy as a clam",
		"On the ropes",
		"Greased lightning",
		"Cut to the chase",
		"Dropping like flies",
		"Keep your eyes peeled",
		"Shot in the dark",
		"Short end of the stick",
		"I smell a rat",
		"What am i, Chopped liver",
		"Needle in a haystack",
		"Beating a dead horse",
		"Jig is up",
		"Right off the bat",
		"Jaws of life",
		"No-brainer",
		"Scot-free",
		"Foaming at the mounth",
		"Keep on truckin'",
		"High and dirty",
		"Hard pill to swallow",
		"Playing for keeps",
		"No ifs, ands, or buts",
		"Two down, one to go",
		"High and dry",
		"Heads up",
		"Give a man a fish",

	};


	static string[] LivingSubjectVerbPlacementNoun = new string[] {
		"butcher",
		"Sliced",
		"the man",

	};

	static string[] AdverbLiving = new string[] {
		"boldly",
		"violently",
		"daintily",
		"mysteriously",
		"bravely",
		"courageously",
		"swiftly",
		"meaningfully",
		"effectively",
		"intensely",
		"wrongly",
		"helpfully",
		"less",
		"nervously",
		"ultimately",
		"tenderly",
		"fervently",
		"knowledgeably",
		"violently",
		"verbally",
		"suddenly",
		"cleverly",
		"jealously",
		"wetly",
		"correctly",
		"absentmindedly",
		"kiddingly",
		"probably",
		"normally",
	};

	static string[] AdverBeforeOrAfter = new string[] {
		"hardly",
	};

	static string[] AdverHow = new string[] {

	};
	static string[] AdverWhen = new string[] {

	};
	static string[] AdverWhere = new string[] {

	};
	static string[] AdverWhy = new string[] {

	};



	static string[] AdjectiveLiving = new string[] {
		"angry",
		"absent",
		"aboriginal",
		"ahead",
		"abashed",

		"bold",
		"brave",
		"berserk",
		"boring",
		"blue",
		"bored",

		"careless",
		"crabby",

		"disgusted",
		"dead",
		"delicious",
		"dramatic",
		"doubtful",
		"disagreeable",

		"equal",
		"excited",
		"electric",

		"funny",
		"fertile",
		"familiar",
		"flawless",
		"filthy",
		"fumbling",

		"glorious",

		"immense",
		"important",
		"inconclusive",

		"likeable",
		"lewd",

		"mysterious",
		"mighty",
		"military",
		"maddening",
		"maniacal",
		"mushy",
		"meek",

		"nifty",

		"obscene",

		"private",
		"petite",

		"red",
		"royal",

		"scarce",
		"skillful",
		"seemly",
		"succinct",
		"strange",
		"swanky",
		"shivering",

		"tidy",

		"uppity",

		"violent",

		"workable",
		"white",

		"yielding",

		"zany",

	};



	static string[] prepositionPlacement = new string[] {
	//	"as",
		"at",
		"behind",
		"beneath",
		"beside",
		"below",
		"by",
		"far from",
		"from",
		"in",
		"in front of",
		"inside",
		"near",
		"next to",
		"on top of",
		"ouside",
		"over",
		"under",
		"underneath",
		"with",
		"within",

	};

	static string[] prepositionItem = new string[] {//preposition , the plate, the paper. something small that you cannot be inside
	//	"as",
	//	"at",
		"behind",
		"beneath",
		"beside",
	//	"below",
	//	"by",
		"far from",
	//	"from",
	//	"in",
		"in front of",
	//	"inside",
		"near",
		"next to",
		"on top of",
	//	"ouside",
		"over",
		"under",
		"underneath",
	//	"with",
	//	"within",

	};

	static string[] nounPlacement = new string[] {//different buildings('The' big ben), locations (london), 
		"the Eiffel Tower",
		"the garden",
		"the Empire State Building",
		"the Thompsons",
		"the Systematic System Stabilizer",
		"harvard",
		"yale",
		"the university",
		"London",
		"the Big Ben",
		"the building",
		"Westerlyn",
		"Ironvale",
		"the Coldhall",
		"Esterwolf",
		"Bellyn",
		"Foxbush",
		"the Whitehaven",
		"Lochcastle",
		"Clearlake",
		"the pinehedge",
		"Snowbay",
	};

	static string[] subject1NonLiving = new string[] {
		"the pot",
		"the floor",
		"the stair",
		"the ball",
		"the roof",
		"the lawn",
		"the tree",
		"the computer",
		"the wire",
		"the transistor",
		"the processor",
		"the monitor",
		"the graphic card",
		"the sound card",
		"the motherboard",
		"the hardware",
		"the metal pile",
		"the plastic lid",
		"the cap",
		"the cape",
		"the lid",
		"the car",
		"the teddybear",
		"the plate",
		"the fork",
		"the knife",
		"the paper",
		"the lego piece",
		"the bed",
		"the pillow",
		"the duvet",
		"the basement",
		"the desk",
		"the pencil",
		"the garbage",
		"the trashcan",
		"the candle",
		"the box",
		"the wood pile",
		"the plank",
		"the rope",
		"the chain",
		"the cannonball",
		"the sword",
		"the wooden leg",
		"the coat",
		"the pirate hat",
		"the goggle",
	};



	static string[] subject2Living = new string[] {//Just A Group Of Living Creatures. Everything From Tiny Viruses To Elephants Works

		"butcher",
		"sharshooter",
		"thug",
		"criminal",
		"slayer",
		"devil",
		"angel",
		"hooligan",
		"punk",
		"squad",
		"soldier",
		"pop",
		"panda",
		"miner",
		"gecko",
		"buster",
		"agent",
		"mermaid",
		"shark",
		"widows",
		"american",
		"german",
		"norwegian",
		"russian",
		"mutant",
		"racoon",
		"duckie",
		"bear",
		"swan",
		"hawk",
		"crow",
		"bumblebee",
		"dog",
		"kitten",
		"horse",
		"pikachu",
		"slug",




	};

	static string[] subject1Living = new string[] {

		"butcher",
		"sharshooter",
		"thug",
		"criminal",
		"slayer",
		"devil",
		"angel",
		"hooligan",
		"punk",
		"squad",
		"boss",
		"employee",
		"doctor",
		"squad",
		"exterminator",
		"brother",
		"uncle",
		"aunt",
		"grandfather",
		"father",
		"mother",
		"sister",
		"little sister",
		"little brother",
		"cousin",
		"grandmother",
		"father in law",
		"mother in law",
		"mom",
		"dad",

	};

	static string[] subject1Thing = new string[] {//As You Can See, This Can Be Anything. The Cupcakes Were Realy Delicious. cupcakes is the subject. 

		"potato chips",
		"cupcakes",
		"jake",
		"boot",
		"flip-flop",
		"man",
		"wind",
		"rain",
		"sleet",
		"hail",
		"rug",
		"carpet",
		"chicken",
		"anyone on the soccer team",

	};

	static string[] subject1LivingMultiple = new string[] {

		"squad",

	};


	//	prezi.com/mtbhbxfo1wyq/inchoative-verb
	static string[] InchoativeVerbsLiving = new string[] {//Verbs That Changes The Objects state
		"tanned",
		"frozen",
		"rotted",
		"decayed",
		"dried",
		"petrified",
		"melted",
		"wilted",
		"hardened",
		"softened",
		"solidified",
		"aged",
		"marinated",
		"faded",
		"evaporated",
		"matured",
		"rose",
		"withered",
	};

	static string[] InchoativeVerbsNonLiving = new string[] {//Verbs That Changes The Objects state
		"rusted",
		"solidified",
		"aged",
		"marinated",
		"faded",
		"evaporated",
		"matured",
		"rose",
		"withered",
	};




	static string[] verb1LivingPast = new string[] {//past - I ran
		"ran",
		"straggled",
		"strolled",
		"slogged",
		"trudged",
		"shuffled",
		"stole",
		"skulked",
		"snuck",
		"slunk",
		"tiptoed",
		"arrived",
		"entered",
		"sprinted",
		"bolted",

	};

	static string[] verb1LivingPresent = new string[] {//present - I AM running
		"running",
		"straggling",
		"strolling",
		"slogging",
		"trudging",
		"shuffling",
		"stealing",
		"skulking",
		"sneaking",
		"slinking",
		"tiptoing",
		"arriving",
		"entering",
		"sprinting",
		"bolting",

	};

	static string[] verb1LivingFuture = new string[] {//future -I WILL run
		"run",
		"straggle",
		"stroll",
		"slog",
		"trudged",
		"shuffle",
		"stolen",
		"skulk",
		"sneak",
		"slink",
		"tiptoe",
		"arrive",
		"enter",
		"sprint",
		"bolt",

	};

	static string[] verb1NonLivingPast = new string[] {
		"fell",
		"exploded",
		"rolled",
		"levetated",
		"bounced",
		"shattered",
	};

	static string[] helpingVerbs = new string[] {
		"be",
		"am",
		"is",
		"are",
		"was",
		"were",
		"been",
		"being",
		"have",
		"has",
		"had",
		"could",
		"should",
		"would",
		"may",
		"might",
		"must",
		"shall",
		"can",
		"will",
		"do",
		"did",
		"does",
		"having",
	};

	static string[] actionVerbs = new string[] {
		"clean",
		"drive",
		"eat",
		"fly",
		"go",
		"live",
		"make",
		"play",
		"read",
		"run",
		"shower",
		"sleep",
		"smile",
		"stop",
	};


	static string[] subjects = new string[] {
		"pierre",
		"the class",
		"the paste",
		"the salad",
		"the milk",
		"the school",
		"the milk",
		"olaf",
		"the table",
		"Mr smith",
		"my son",
		"uncle tom",
	};

	static string[] transitiveActiveVerb = new string[] {//
		"bring",
		"send",
		"owe",
		"contain",
		"buy",
		"show",
		"take",
		"tell",
		"verify",
		"check",
		"get",
		"wash",
	};

	static string[] linkingVerbs = new string[] {//
		"appear",
		"become",
		"feel",
		"grow",
		"look",
		"seem",
		"remain",
		"smell",
		"sound",
		"stay",
		"taste",
		"turn",
	};

	static string[] linkingVerbsToBe = new string[] {//
		"be",
		"am",
		"is",
		"are",
		"was",
		"were",
		"been",
		"being",
	};


















	static string[] nonLivingObjects = new string[] {
		"train",
		"floor",
		"roof",
		"wall",
		"tree",
		"lamp",
		"tent",
		"ball",
		"achelousaurus",
		"alamosaurus",
		"ammosaurus",
		"wamble",
		"glabella",
		"lunule",
		"ferrule",
		"lemniscate",
		"car",
		"blanket",
		"mountain",
		"Mamihlapinatapai",
		"Gynecomastia",
	};

	static string[] livingObjects = new string[] {
		"man",
		"woman",
		"kid",
		"dog",
		"cat",
		"dragon",
		"lizard",
		"bacteria",
		"insect",
		"bird",
		"achelousaurus",
		"alamosaurus",
		"ammosaurus",
	};

	static string[] nonLivingBehaviour = new string[] {
		"fell",
		"levitated",
		"flew",
		"dissapeared",
		"rolled",
		"spinned",
		"exploded",
		"vanished",
		"desintegrated",
		"grew",
		"lay",
	};


	static string[] livingBehaviour = new string[] {
		"ran",
		"walked",
		"cycled",
		"drove",
		"lied",
		"slept",
		"woke",
		"ate",
		"thought",
		"whispered",
		"yelled",
		"pointed",
		"talked",
		"scratched",
	};




	static string[] placements = new string[] {
		"onto",
		"into",
		"over",
		"under",
		"beside",
		"between",
		"behind",
		//	"to the right of",
		//"to the left of",
		//"far from",
		//"in front of",
		//"on top of",
		//"close to",
		//"next to",
	};

	static string[] helpingVerbs1 = new string[]{
		"is",
		"was",
	};

	static string[] helpingVerbs2 = new string[]{
		"are",
		"were",
		"do",
		"did",
	};


	static string[] helpingVerbPerson = new string[]{
		"has",
	};
	static string[] helpingVerbPersons = new string[]{
		"have",
	};

	static string[] helpingVerbs3 = new string[]{
		"has",
		"have",
	};

	static string[] helpingVerbs3v1 = new string[]{
		"been",
		"had",
	};


	static string[] helpingVerbslast = new string[]{
		"could",
		"should",
		"would",
		"may",
		"might",
		"must",
		"shall",
		"can",
		"will",
		"do",
		"did",
		"does",
	};

	static string[] helpingVerbs4 = new string[]{
		"is",
		"was",

		"are",
		"were",


		"being",
		"having",


	};

	static string[] helpingVerbs5 = new string[]{
		"is",
		"was",
		"has",

		"have",
		"are",
		"were",

		"had",
		"do",
		"did",


		"be",
		"been",
		"being",
		"could",
		"should",
		"would",
		"may",
		"might",
		"must",
		"shall",
		"can",
		"will",
		"does",
		"having",

	};








	static string[] describingss = new string[] {
		"Aitgas",
		"Alcoa",
		"Americo",
		"Broadwing",
		"Centex",
		"Unity",
		"Apple",
		"HTC",
		"Comerica",
		"Cooper",
		"Earthlink",
		"Edison",
	};

	static string[] describings = new string[] {
		"CEO",
		"janitor",
		"worker",
		"VIP guest",
		"VIP",
		"host",
		"employee",
		"doctor",
		"shoplifter"
	};

	static string[] TTest1 = new string[] {
		"system",
		"stabilizer",
		"liquid",
		"fire",
		"nerve",
		"production",
		"concern",
		"issue",
		"conduct",
		"stocks",
		"appartment",
		"property",
		"concept",
		"court",
		"vain",
		"coast",
	};

	static string[] TTest2 = new string[] {
		"made",
		"bought",
		"sold",
	};


	static string[] TTest3 = new string[] {
		"hughe",
		"small",
		"horrible",
		"awesome",
		"astronomical",
	};


	static string[] TTest4 = new string[] {
		"show",
		"fotball",
		"missile",
		"fire",
	};


	static string[] Items = new string[] {
		"mechanic",
		"doctor",
		"teacher",
		"firefigher",
		"table",
		"chair",
		"gold",
		"system",
		"stabilizer",
		"sphere",
		"wireless",
		"structure",
		"sentence",
		"book",
		"paper",
		"air",
		"plane",
		"train",
		"steam",
		"electricity",
		"mining",
		};

	static string[] TheTheWord = new string[] {
		"The",
	};

	static string[] test1 = new string[] {
		"Vita",
		"Coop",
		"StarWars",
		"The Starlight roses",
		"The Wizzard camp",
		"The bear",
		"The Sword",
		"DDC designs",
		"The systematic system stabializor",
		"Clank blanks CCS",
		"ZQP foods",
	};

	static string[] describing = new string[] {
		"CEO",
		"Janitor",
		"worker",
		"VIP guest",
		"VIP",
		"host",
		"employee",
		"box",
		"bottle",
		"paper",
		"entrence",
	};

	static string[] test2 = new string[] {
		"tasted",
		"smelled",
		"looked",
		"sounded",
		"stood",
		"felt",
		"raised",
		"sank",
		"walked the doll",
		"listened",
	};
	static string[] test3 = new string[] {
		"horribly",
		"neutraly",
		"sinseraly",
		"awfully",
		"disgracefully",
		"respectfully",
	};
	static string[] test4 = new string[] {
		"ok",
		"good",
		"bad",
		"sour",
		"sweet",
	//	"mad",
		"glad",
		"nice",
		"weird",
	};

	static string[] Adjective = new string[] {
		"horrible",
		"adorable",
		"accurate",
		"adorable",
		"aggressive",
		"alienated",
		"amazing",
		"awful",
		"beautiful",
		"bewitched",
		"bitter",
		"bleak",
		"blond",
		"bossy",
		"brave",
		"broken",

	};

	static string[] AdjectivePast = new string[] {
		"adored",
		"alienated",
		"bewitched",
		"broke",
		"carefree",
		"crushed",
		"confused",
		"cooked",
		"damaged",

	};

}*/

#endregion