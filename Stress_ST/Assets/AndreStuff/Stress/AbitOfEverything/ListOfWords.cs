using UnityEngine;
using System.Collections;

public class ListOfWords {

	//This is straight forward ;-p the words are created and sendt to their target

	const string LowerCaseLetters = "abcdefghijklmnopqrstuvwxyz";
	const string UpperCaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
	const string Numbers = "0123456789";
	const string NormalSpecialSymbols = "!#%&/()=?+-.,@$";	
	const string AllSpecialSymbols = "!#¤%&/()=?-:;,._'}+][{€$£@|§<>";

	const string UpperAndLowerCaseLetters = 									"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
	const string UpperLowerCaseAndNumbers = 						  "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
	const string UpperLowerCaseNumbersAndNormalSymbols =   "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!#%/()=?+-@";
	const string AllLettersAndSymbols = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!#¤%&/()=?-:;,._'}+][{€$£@|§<>";

	#region ListOfWords

	#region ThreeLetterWords
		static string[] _ThreeLetterWords = new string[] {//3 letters
			"and",
			"ant",	
			"any",	
			"air",
			"aim",
			"aid",		
			"all",
			"add",	
			"ape",
			"ark",
			"art",	
			"arm",
			"arc",		
			"are",
			"act",
			"axe",	
			"awl",	
			"age",	
			"ago",		
		};
	#endregion

	#region FourLetterWords
	static string[] _FourLetterWords = new string[] {//4 letters
			"also",	
			"ally",	
			"able",	
			"acid",		
			"axis",	
			"away",	
			"anti",	
			"area",	
			"aunt",	
			"auto",
			"atom",
		};
	#endregion

	#region FiveLetterWords
	static string[] _FiveLetterWords = new string[] {//5 letters
			"admit",
			"adult",	
			"adept",	
			"addon",	
			"added",	
			"adder",	
			"alarm",	
			"along",	
			"alpha",	
			"altar",	
			"alone",	
			"align",	
			"alien",	
			"along",
			"alike",	
			"album",	
			"alert",
			"alibi",	
			"aline",	
			"allow",
			"alley",
			"alive",	
			"anger",	
			"angle",	
			"angry",	
			"anime",	
			"ankle",	
			"apart",	
			"apply",
			"april",	
			"armor",	
			"aroma",	
			"array",
			"arrow",	
			"arena",
			"aware",	
			"awake",
			"award",	
			"awful",	
			"about",
			"above",	
			"abyss",	
			"actor",
			"amber",	
			"amiss",	
			"amuse",	
			"among",	
			"ample",	
			"attic",
			"audio",	
			"aside",	
			"agile",	
			"agree",	
			"agent",	
			"ahead",	
			"after",
			"anvil",		
			"asset",
		};
	#endregion

	#region SixLetterWords
	static string[] _SixLetterWords = new string[] {//6 letters
			"accent",
			"accept",
			"access",
			"angler",
			"animal",
			"adware",
			"adverb",
			"advice",
			"appear",
			"asthma",
			"asylum",
			"around",
			"arrest",
			"autumn",
			"avenue",
			"across",
			"always",
			"arctic",
			"aboard",
			"actual",
			"action",
			"active",
			"acting",
			"affair",
			"afford",
			"affect",
			"afraid",
			"aerial",
			"always",
			"annual",
			"answer",
			"anthem",
			"artist",
			"ascend",
			"atomic",
			"attain",
			"attend",
			"abroad",
			"abrupt",
			"absent",
			"absorb",
			"absurd",
			"adjust",
			"agenda",
			"agreed",
			"agency",
			"amount",
			"analog",
			"ananas",
			"anchor",
			"anyhow",
			"anyway",
			"anyone",
			"argent",
			"archer",
			"aspect",
			"assist",
			"august",
			"auntie",
			"author",
		};
	#endregion

	#region SevenLetterWords
	static string[] _SevenLetterWords = new string[] {//7 letters
			"abandon",
			"achieve",
			"advisor",
			"account",
			"airline",
			"advance",
			"airship",
			"already",
			"animate",
			"airport",
			"arrival",
			"armoury",
			"apology",
			"autopsy",
			"average",
			"allowed",
			"archive",
			"ability",
			"acquire",
			"actress",
			"amazing",
			"annoyed",
			"antenna",
			"another",
			"apricot",
			"article",
			"athlete",
			"attempt",
			"awesome",
			"awfully",
			"awkward",
			"absence",
			"academy",
			"adapter",
			"address",
			"against",
			"agility",
			"ageless",
			"allergy",
			"amnesty",
			"amusing",
			"analyst",
			"anarchy",
			"anchovy",
			"ancient",
			"antique",
			"anxiety",
			"anymore",
			"anxious",
			"anybody",
			"anytime",
			"archery",
			"asocial",
			"auction",
			"auditor",
			"augment"
		};
	#endregion

	#region EightLetterWords
	static string[] _EightLetterWords = new string[] {//8 letters
			"accuracy",
			"aircraft",
			"airplane",
			"alarming",
			"accurate",
			"although",
			"anywhere",
			"aluminum",
			"alphabet",
			"arrogant",
			"argument",
			"armchair",
			"asteroid",
			"appetite",
			"apparent",
			"allowing",
			"archived",
			"assemble",
			"actually",
			"activity",
			"aerobics",
			"aerofoil",
			"alienist",
			"ambiance",
			"ambition",
			"ambitous",
			"announce",
			"annually",
			"annoying",
			"antelope",
			"approach",
			"aquatics",
			"aquapark",
			"approval",
			"aptitude",
			"artefact",
			"athletic",
			"atrocity",
			"attitude",
			"absolute",
			"abstract",
			"abundant",
			"academic",
			"adaptive",
			"addicted",
			"addition",
			"adequate",
			"afernoon",
			"alliance",
			"analyzer",
			"analyser",
			"anatomic",
			"ancestry",
			"analogue",
			"analysis",
			"ancestor",
			"antibody",
			"antipode",
			"anyplace",
			"antihero",
			"anything",
			"assassin",
			"assembly",
			"aspargus",
			"aspirate",
			"allusion",
			"audience",
		};
	#endregion

	#region NineLetterWords
	static string[] _NineLetterWords = new string[] {//9 letters
			"abandoned",
			"adversary",
			"admission",
			"according",
			"aluminium",
			"alongside",
			"alternate",
			"astronomy",
			"apartment",
			"apparatus",
			"appetizer",
			"automatic",
			"available",
			"adbuction",
			"affiliate",
			"affection",
			"algorithm",
			"alchemist",
			"amendment",
			"ambulance",
			"anonymous",
			"announced",
			"anonymity",
			"applicant",
			"appositio",
			"artillery",
			"artichoke",
			"attendant",
			"attention",
			"attribute",
			"awareness",
			"abundance",
			"absurdity",
			"adaptable",
			"afterward",
			"agreeable",
			"aggravate",
			"allowance",
			"alligator",
			"allowedly",
			"ancestors",
			"amusement",
			"arbitrary",
			"archfiend",
			"archenemy",
			"associate",
			"assistant",
			"autofocus",
			"audiobook",
		};
	#endregion

	#region TenLetterWords
	static string[] _TenLetterWords = new string[] {//10 letters
			"accessible",
			"accordance",
			"accountant",
			"acceptable",
			"accounting",
			"acceptance",
			"admiration",
			"altogether",
			"associated",
			"apparently",
			"apocalypse",
			"assumption",
			"arithmetic",
			"asymmetric",
			"automation",
			"appearance",
			"automobile",
			"autonomous",
			"accomplish",
			"acrobatics",
			"activation",
			"affordable",
			"ambassador",
			"ammunition",
			"annihilate",
			"antagonist",
			"appreciate",
			"artificial",
			"atmosphere",
			"attractive",
			"attachment",
			"attendance",
			"attraction",
			"abridgment",
			"absolutely",
			"absorptive",
			"accelerate",
			"absorption",
			"adjustment",
			"addressing",
			"additional",
			"adjustable",
			"aggressive",
			"afterwards",
			"allocation",
			"analytical",
			"assistance",
			"assessment",
			"assignment",
		};
	#endregion

	#region ElevenLetterWords
	static string[] _ElevenLetterWords = new string[] {//11 letters
			"apocalyptic",
			"arrangement",
			"alternative",
			"accessories",
			"accompanied",
			"applicaiton",
			"assassinate",
			"achievement",
			"acupuncture",	
			"acknowledge",
			"affirmative",
			"appointment",
			"appreciated",
			"appropriate",
			"attestation",
			"atmospheric",
			"awestricken",
			"accelerator",
			"academician",
			"aftergrowth",
			"agriculture",
			"antibiotics",
			"archaeology",
			"assassinate",
		};
	#endregion

	#region TwelveLetterWords
	static string[] _TwelveLetterWords = new string[] {//12 letters
			"alphabetical",
			"astronomical",
			"accidentally",
			"accumulation",
			"administator",
			"alphanumeric",
			"astrophysics",
			"announcement",
			"artistically",
			"articulation",
			"abstractness",
			"acceleration",
			"adaptability",
			"agricultural",
			"afterfeather",
			"aggressively",	
			"anticipation",
		};
	#endregion

	#region ThirteenLetterWords
	static string[] _ThirteenLetterWords = new string[] {//13 letters
			"argyranthemum",
			"approximately",
			"arteriography",
			"anticlockwise",
			"archaelogical",
			"authorization",
		};
	#endregion

	#region FourteenLetterWords
	static string[] _FourteenLetterWords = new string[] { //14 letters
			"alphabetically",
			"automatization",
			"automatisation",
			"administration",
			"administrative",
		};
	#endregion

	#region FifteenLetterWords
	static string[] _FifteenLetterWords = new string[] { //15 letters
			"acupuncturation",
			"anticlockwisely",
			"archaeastronomy",
		};
	#endregion

	#region SixteenLetterWords
	static string[] _SixteenLetterWords = new string[] { //16 letters
			"administratively",
		};	
	#endregion

	#endregion
	public static string GetRandomWords(int letters){//returns a word of size letters
		if (letters == 3) {
			return _ThreeLetterWords [Random.Range (0, _ThreeLetterWords.Length)];
		} else if (letters == 4) {
			return _FourLetterWords [Random.Range (0, _FourLetterWords.Length)];
		} else if (letters == 5) {
			return _FiveLetterWords [Random.Range (0, _FiveLetterWords.Length)];
		} else if (letters == 6) {
			return _SixLetterWords [Random.Range (0, _SixLetterWords.Length)];
		} else if (letters == 7) {
			return _SevenLetterWords [Random.Range (0, _SevenLetterWords.Length)];
		} else if (letters == 8) {
			return _EightLetterWords [Random.Range (0, _EightLetterWords.Length)];
		} else if (letters == 9) {
			return _NineLetterWords [Random.Range (0, _NineLetterWords.Length)];
		} else if (letters == 10) {
			return _TenLetterWords [Random.Range (0, _TenLetterWords.Length)];
		} else if (letters == 11) {
			return _ElevenLetterWords [Random.Range (0, _ElevenLetterWords.Length)];
		} else if (letters == 12) {
			return _TwelveLetterWords [Random.Range (0, _TwelveLetterWords.Length)];
		} else if (letters == 13) {
			return _ThirteenLetterWords [Random.Range (0, _ThirteenLetterWords.Length)];
		} else if (letters == 14) {
			return _FourteenLetterWords [Random.Range (0, _FourteenLetterWords.Length)];
		} else if (letters == 15) {
			return _FifteenLetterWords [Random.Range (0, _FifteenLetterWords.Length)];
		} else if (letters == 16) {
			return _SixteenLetterWords [Random.Range (0, _SixteenLetterWords.Length)];
		} else {
			return _SixteenLetterWords [Random.Range (0, _SixteenLetterWords.Length)];
			//return GetRandomLowerCaseLetters(letters);
		}
	}
		
	#region TotalyRandomMethods
	public string GetRandomLowerCaseLetters(int letters){
		string TheWord = "";
		for (int i = 0; i < letters; i++) {
			TheWord += LowerCaseLetters [Random.Range (0, LowerCaseLetters.Length)];
		}
		return TheWord;
	}

	public string GetRandomUpperCaseLetters(int letters){
		string TheWord = "";
		for (int i = 0; i < letters; i++) {
			TheWord += UpperCaseLetters [Random.Range (0, UpperCaseLetters.Length)];
		}
		return TheWord;
	}

	public string GetRandomNumbers(int letters){
		string TheWord = "";
		for (int i = 0; i < letters; i++) {
			TheWord += Numbers [Random.Range (0, Numbers.Length)];
		}
		return TheWord;
	}

	public string GetRandomNormalSpecialCharacters(int letters){
		string TheWord = "";
		for (int i = 0; i < letters; i++) {
			TheWord += NormalSpecialSymbols [Random.Range (0, NormalSpecialSymbols.Length)];
		}
		return TheWord;
	}

	public string GetRandomAlSpecialCharacters(int letters){
		string TheWord = "";
		for (int i = 0; i < letters; i++) {
			TheWord += AllSpecialSymbols [Random.Range (0, AllSpecialSymbols.Length)];
		}
		return TheWord;
	}

	public string GetRandomUpperAndLowerCaseLetters(int letters){
		string TheWord = "";
		for (int i = 0; i < letters; i++) {
			TheWord += UpperAndLowerCaseLetters [Random.Range (0, UpperAndLowerCaseLetters.Length)];
		}
		return TheWord;
	}

	public string GetRandomUpperLowerCaseAndNumbers(int letters){
		string TheWord = "";
		for (int i = 0; i < letters; i++) {
			TheWord += UpperLowerCaseAndNumbers [Random.Range (0, UpperLowerCaseAndNumbers.Length)];
		}
		return TheWord;
	}

	public string GetRandomUpperLowerCaseNumbersAndNormalSymbols(int letters){
		string TheWord = "";
		for (int i = 0; i < letters; i++) {
			TheWord += UpperLowerCaseNumbersAndNormalSymbols [Random.Range (0, UpperLowerCaseNumbersAndNormalSymbols.Length)];
		}
		return TheWord;
	}

	public string GetRandomEverything(int letters){
		string TheWord = "";
		for (int i = 0; i < letters; i++) {
			TheWord += AllLettersAndSymbols [Random.Range (0, AllLettersAndSymbols.Length)];
		}
		return TheWord;
	}
	#endregion

}
