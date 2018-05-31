using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AllOurWords {

	static List<string> TheSentence = new List<string> ();
	static string saver = "";
//	static int placement = 0;

	public static List<string> sentencetypeone(){
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
	
		/*Debug.Log (
			test1 [Random.Range (0, test1.Length)] + " " +
			test2 [Random.Range (0, test2.Length)] + " " +
			test3 [Random.Range (0, test3.Length)] + " " +
			test3 [Random.Range (0, test3.Length)] + " " +
			test4 [Random.Range (0, test4.Length)]
		);
		Debug.Log (
			test1 [Random.Range (0, test1.Length)] + " " +
			test2 [Random.Range (0, test2.Length)] + " " +
			test3 [Random.Range (0, test3.Length)] + " " +
			test3 [Random.Range (0, test3.Length)] + " " +
			test4 [Random.Range (0, test4.Length)]
		);
		Debug.Log (
			test1 [Random.Range (0, test1.Length)] + " " +
			test2 [Random.Range (0, test2.Length)] + " " +
			test3 [Random.Range (0, test3.Length)] + " " +
			test3 [Random.Range (0, test3.Length)] + " " +
			test4 [Random.Range (0, test4.Length)]
		);
		Debug.Log (
			test1 [Random.Range (0, test1.Length)] + " " +
			test2 [Random.Range (0, test2.Length)] + " " +
			test3 [Random.Range (0, test3.Length)] + " " +
			test3 [Random.Range (0, test3.Length)] + " " +
			test4 [Random.Range (0, test4.Length)]
		);
		Debug.Log (
			test1 [Random.Range (0, test1.Length)] + " " +
			test2 [Random.Range (0, test2.Length)] + " " +
			test3 [Random.Range (0, test3.Length)] + " " +
			test3 [Random.Range (0, test3.Length)] + " " +
			test4 [Random.Range (0, test4.Length)]
		);*/

		TheSentence.Clear();

/*		TheSentence.Add (Adjective [Random.Range (0, Adjective.Length)]);
		TheSentence.Add (AdjectivePast [Random.Range (0, AdjectivePast.Length)]);
		TheSentence.Add (Adjective [Random.Range (0, Adjective.Length)]);
		return TheSentence;*/

	/*	TheSentence.Add ("The");
		TheSentence.Add (Items [Random.Range (0, Items.Length)]);
		TheSentence.Add (Items [Random.Range (0, Items.Length)]);
		TheSentence.Add ("and");
		TheSentence.Add (Items [Random.Range (0, Items.Length)]);
		TheSentence.Add ("Corp");
		return TheSentence;*/


	/*	TheSentence.Add (TTest1 [Random.Range (0, TTest1.Length)]);
		TheSentence.Add (TTest2 [Random.Range (0, TTest2.Length)]);

		saver = TTest3 [Random.Range (0, TTest3.Length)];

		if (saver [0] == 'a' || saver [0] == 'e' || saver [0] == 'i' || saver [0] == 'o' || saver [0] == 'u' || saver [0] == 'i') {
			TheSentence.Add ("an");
		} else {
			TheSentence.Add ("a");
		}
		TheSentence.Add (saver);

		TheSentence.Add (TTest4 [Random.Range (0, TTest4.Length)]);*/





/*		TheSentence.Add ("The");
		TheSentence.Add (nonLivingObjects [Random.Range (0, nonLivingObjects.Length)]);
		TheSentence.Add (nonLivingBehaviour [Random.Range (0, nonLivingBehaviour.Length)]);
		TheSentence.Add (placements [Random.Range (0, placements.Length)]);
		TheSentence.Add ("the");
		TheSentence.Add (nonLivingObjects [Random.Range (0, nonLivingObjects.Length)]);
*/

	

	/*	TheSentence.Add ("of");

		TheSentence.Add (Adjective [Random.Range (0, Adjective.Length)]);
		TheSentence.Add (describingss [Random.Range (0, describingss.Length)]);*/


	
		string a = "";
		foreach (string s in TheSentence){
			a += s + " ";
		}
		Debug.Log(a);
		return TheSentence;

		if (Random.Range (0, 4) == 0) {//the bear | smelled | sinseraly | awfully | good
			
			TheSentence.Add (test1 [Random.Range (0, test1.Length)]);
			TheSentence.Add (test2 [Random.Range (0, test2.Length)]);
			TheSentence.Add (test3 [Random.Range (0, test3.Length)]);
			TheSentence.Add (test4 [Random.Range (0, test4.Length)]);
		
		} else if (Random.Range (0, 4) == 0) {

			TheSentence.Add (test1 [Random.Range (0, test1.Length)]);
			TheSentence.Add (Adjective [Random.Range (0, Adjective.Length)]);
			TheSentence.Add (describing [Random.Range (0, describing.Length)]);
			TheSentence.Add (test2 [Random.Range (0, test2.Length)]);
			TheSentence.Add (test3 [Random.Range (0, test3.Length)]);
			TheSentence.Add (test4 [Random.Range (0, test4.Length)]);
			
		} else if (Random.Range (0, 4) == 0) {

			TheSentence.Add (TheTheWord [0]);
			TheSentence.Add (Adjective [Random.Range (0, Adjective.Length)]);
			TheSentence.Add (describing [Random.Range (0, describing.Length)]);
			TheSentence.Add (test2 [Random.Range (0, test2.Length)]);
			TheSentence.Add (test4 [Random.Range (0, test4.Length)]);
			
		} else if (Random.Range (0, 4) == 0) {

			TheSentence.Add (test4 [Random.Range (0, test4.Length)]);
			TheSentence.Add (test2 [Random.Range (0, test2.Length)]);
			TheSentence.Add (TheTheWord [0]);
			TheSentence.Add (Adjective [Random.Range (0, Adjective.Length)]);
			TheSentence.Add (describing [Random.Range (0, describing.Length)]);

		} else {
		
			TheSentence.Add ("The");
			TheSentence.Add (nonLivingObjects [Random.Range (0, nonLivingObjects.Length)]);
			TheSentence.Add (nonLivingBehaviour [Random.Range (0, nonLivingBehaviour.Length)]);
			TheSentence.Add (placements [Random.Range (0, placements.Length)]);
			TheSentence.Add ("the");
			TheSentence.Add (nonLivingObjects [Random.Range (0, nonLivingObjects.Length)]);

		}

		return TheSentence;

	//	return "The " + TheObjects [Random.Range (0, TheObjects.Length)] + " " + RelativePronoun [Random.Range (0, RelativePronoun.Length)] + " " + PersonName [Random.Range (0, PersonName.Length)] 
//			    + " " + Verbs2 [Random.Range (0, Verbs2.Length)] + ", " + CorrelativeConjunctions [placement] + " " + Verbs [Random.Range (0, Verbs.Length)] + " " + CorrelativeConjunctions [placement + 1] + " " + Verbs [Random.Range (0, Verbs.Length)];
	}
		


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
		/*	"to the right of",
		"to the left of",
		"far from",
		"in front of",
		"on top of",
		"close to",
		"next to",*/
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

	static string[] helpingVerbs5= new string[]{
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
		



	/*static string[] RelativePronouns = new string[] {
		"tasted",
		"smelled",
		"looked",
	};*/

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
	//kjøkken og hjem selger mange fine ting

	// 	ting 			
	//personliggjøre objecter.  stein selger sko. går ikke.     steinen selger sko går
	//personliggjøre objecter.  stein jogge sko. går ikke.     steinen jogger sko går





/*	static string[] RelativePronoun = new string[] {
		"who",
		"which",
		"that",
	//	"whose",
		"whom",
	};*/
	
/*	static string[] Placements = new string[] {
		"on",
		"over",
		"past",
		"before",
		"in front of",
		"beside",
		"inside",
		"under",
		"to the right",
		"to the left",
		"north",
		"south",
		"west",
		"east",
	};*/


/*	static string[] TheObjects = new string[] {
		"woman",
		"man",
		"dinosaur",
		"dog",
		"spider",
		"wheel",
		"flame",
		"boat",
		"company",
		"fountain",
		"statue",
		"bear",
	};*/

/*	static string[] TheObjectsPast = new string[] {
		"women",
		"men",
		"dinosaurs",
		"dogs",
		"spiders",
		"wheels",
		"flames",
		"boats",
	};*/


/*	static string[] PersonName = new string[] {
		"Tom",
		"Lara",
		"Olav",
		"Olaf",
		"Clark",
		"Kent",
		"Peter",
		"Adam",
		"Henry",
		"Tori",
	};*/

/*	static string[] CoordinatingConjuntions = new string[] {
		"for",
		"and",
		"nor",
		"but",
		"or",
		"yet",
		"so",

	};*/


/*	static string[] SubordinatingConjunctions = new string[] {
		"after",
		"although",
		"as",
		"as if",
		"as long as",
		"as much as",
		"as soon as",
		"as though",
		"because",
		"before",
		"by the time",
		"even if",
		"even though",
		"if",
		"in order that",
		"in case",
		"lest",

	};*/


/*	static string[] CorrelativeConjunctions = new string[] {
		"both",
		"and",
		"either",
		"or",
		"neither",
		"nor",

	};*/


/*	static string[] Verbs = new string[] {
		"ran",
		"walked",
		"punched",
		"talked",
		"saw",
		"ate",
		"created",
	};*/
/*	static string[] Verbs2 = new string[] {
		"walked",
		"punched",
		"saw",
		"ate",
		"liked",
		"drove",
		"crushed",
		"created",
	};*/

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

}
