<a id="#top"></a>
# DMSConvert

DMSConvert is a small addon for Kerbal Space Program that adds a GUI for
converting from decimal degrees to degrees/minutes/seconds (DMS) format.

**[Download the latest release][latest]**

### Table of Contents

1. [Installation](#installation)
2. [Usage](#usage)
3. [Privacy](#privacy)
4. [Problems](#problems)
5. [Suggestions](#suggestions)

---

### Installation

You can get a copy of DMSConvert in two different ways:

- Download the source code (everything in this repository) from [here][repo-dl]
- Download a release (*only* the folder containing the addon) from [here][releases]

Once you have your copy of DMSConvert:

1. Extract the ZIP file (just remember where you put it!)
2. Copy the DMSConvert folder into your KSP GameData folder
3. Run KSP - the addon will check and create config files when you run KSP

After you have run KSP at least once with the addon installed, the file
structure should look like this:

```
KSP installation 
|-- ...
|-- GameData
|------ DMSConvert
|---------- config.cfg
|---------- DMSConvert.dll
|------ ...
|-- ...
```

[Back to top][]

---

### Usage

The DMSConvert window is only available in game scenes where time passes;
i.e. the KSC overview scene, in the Tracking Center, and in flight.

**To show/hide the DMSConvert window:** Press Alt+U  
**To convert decimal degrees to DMS:** Enter a value in the `Decimal` box
and click the `-->` button  
**To convert DMS to decimal degrees:** Enter values in the correct boxes
on the `D M S` side and click the `<--` button

[Back to top][]

---

### Privacy

DMSConvert is entirely GUI: it doesn't interact with your game in any way
that's invisible to the player.  
The only action it performs other than conversion is config file handling;
additionally, any action that DMSConvert performs is logged in the Alt+F2
debug log.

Per the KSP forums' [Add-on Posting Rules][], DMSConvert only modifies files
inside its own data folder (`<KSP>\GameData\DMSConvert`). As of [v1.0][],
the only file that DMSConvert modifies is its `config.cfg`.

DMSConvert does not gather any data about you, your game, or your computer.

[Back to top][]

---

### Problems

If you experience a problem, question, complaint, error, bug, or glitch
while using DMSConvert, please take the following steps:

If the problem is directly related to DMSConvert's GUI or conversion,
[file an issue on GitHub][issues]:

1. Click the green "New issue" button in the top-right
2. Give the issue a meaningful title
	- unhelpful: `"bad issue pls help!!"`
	- helpful: `"Clicking '-->' button crashes KSP"`
3. Give a little information about the problem
	- Required:
		- Describe the problem
		- Describe what you were doing when the problem occurred
		- List all mods that you currently have installed
	- Optional, but will make help much quicker and may be requested:
		- Paste the contents of your log file, located at `<KSP>\KSP_Data\output_log.txt`,
		where `<KSP>` is the location of your KSP installation
4. Give the issue report a label on the right

[Back to top][]

---

### Suggestions

If you have a feature that you'd like to suggest, let me know! Follow the same
steps as listed above in [Problems](#problems) to submit an [issue report][issues];
just be sure to add the "suggestion" label to your report!

Alternatively, I'm almost always online on [EsperNet IRC][]. Just drop me a PM
about your suggestion, and I'll get back to you if I'm online. **NOTE**, though:
I'll probably end up creating an issue report based on your suggestion anyway,
if I think it's feasible.

[Back to top][]

---

DMSConvert is &copy; 2014 ChaoticWeg (Shawn Lutch). [License][]  
Kerbal Space Program is &copy; 2011-2014 Squad. All Rights Reserved.

DMSConvert uses [code by xEvilReeperx][KSPAddonImproved], which is
graciously provided in the public domain.


[latest]: https://github.com/ChaoticWeg/KSP-DMSConvert/releases/latest
[repo-dl]: https://github.com/ChaoticWeg/KSP-DMSConvert/archive/master.zip
[releases]: https://github.com/ChaoticWeg/KSP-DMSConvert/releases
[v1.0]: https://github.com/ChaoticWeg/KSP-DMSConvert/releases/tag/v1.0
[issues]: https://github.com/ChaoticWeg/KSP-DMSConvert/issues

[License]: https://raw.githubusercontent.com/ChaoticWeg/KSP-DMSConvert/master/license.md
[KSPAddonImproved]: https://github.com/ChaoticWeg/KSP-DMSConvert/blob/master/KSPAddonImproved.cs

[Back to top]: #top
[Add-on Posting Rules]: http://forum.kerbalspaceprogram.com/threads/87841-Add-on-Posting-Rules-July-24th-2014-going-into-effect-August-21st-2014%21
[EsperNet IRC]: http://webchat.esper.net/