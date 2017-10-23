# Smart-Art-Interpreter

Barrierefreiheit ist ein wichtiges Thema und kann mit Hilfe von Technologie unterstütz werden. Besonders im akademischen Bereich werden tagtäglich viele Präsentationen gehalten. Um auch sehbehinderten und blinden Menschen den Zugang zu Präsentationsmaterialen zu gewährleisten, werden Präsentationen oft noch händisch in das Markdown-Format übertragen.  
Der SmartArt-Interpreter ist Bestandteil des Markdown-Converters, welcher PowerPoint Präsentationen nahezu vollständig in korrekte Markdown-Dokumente umwandeln kann. Dabei hat der SmartArt-Interpreter die Aufgabe, SmartArt-Grafiken vollständig zu beschreiben. Dazugehören die textuellen Inhalte, die Beziehungen der einzelnen Inhalte zueinander und die Eigenschaften dieser Grafiken. Die nachfolgende Abbildung zeigt eine Auswahl an bekannten SmartArt-Grafiken:

![smartArts](https://github.com/Juliiia/Smart-Art-Interpreter/blob/master/img/smartarts.png)

## Funktionalität
Sobald der SmartArt-Interpreter als Add-In in PowerPoint installiert wurde. Stehen dem Nutzer folgende Funktionalitäten zur Auswahl:

**Alle SmartArt-Beschreibungen:**
Es gibt verschiedene SmartArt-Grafiken, welche in Kategorien eingeteilt sind. Bevor eine Grafik in einen Markdown-Text konvertiert werden kann, muss einmalig eine Rahmenbeschreibung erstellt werden. Diese Rahmenbeschreibung beinhaltet Platzhalter, welche später während der Konvertierung ersetzt werden. <br/>
In dem öffnenden Dialog sind alle SmartArt-Grafiken eingetragen, zu welchen eine Beschreibung mit Platzhaltern existiert. Über das Anklicken einer Grafik kann die Rahmenbeschreibung eingesehen und bearbeitet werden.

**Konvertieren:**
Dies startet die eigentliche Konvertierung aller SmartArt-Grafiken in der geöffneten PowerPoint-Präsentation. Die Beschreibungen werden separat im gleichen Verzeichnis wie diese Präsentation gespeichert.<br/>
Wenn eine SmartArt-Grafik gefunden wird, zu welcher noch keine Rahmenbeschreibung existiert, erscheint ein Dialog und bietet das Anlegen und Speichern dieser Rahmenbeschreibung an. 

**Zielordner öffnen:**
Das Verzeichnis in dem die Präsentation und die generierten Beschreibungen liegen, wird geöffnet.
