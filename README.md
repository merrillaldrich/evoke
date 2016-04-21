# evoke
Pictograph to Text to Speech tablet app for people who are speech impaired.

This project grew out of a need from someone in my family who suffers from Aphasia and Progressive Supranuclear Palsy. 
The short story is she will lose her ability to speak. The application has these basic goals:

1. Convert pictographs to text and the text to speech
2. Do that with touch, on a tablet
3. Make it possible to add vocabulary and additional pictures easily by dropping files in a folder, without recompiling/reinstalling

Technically the project is built with c# / Visual Studion 2015 Community / Xamarin forms, targeting Windows 10 first, then possibly 
other tablet OSs. Pictographs in use all come from master images that are SVG, created in Inkscape.

It's a very unstable little 0.0.01 demo at this point, but fundamentally works.

The application provides a layout of images in a scrollable grid, divisible into categories. The images and the vocabulary
and categories are all loaded at run time from disk, by design, using the PCLStorage library. The vocabulary is defined by a very
simple tab delimited text file where each line is a word / category / image combination. The listed images are expected to 
be available in the same folder. This enables delivering more image/word combinations with file sharing.

On Windows the storage location for these files is \Users\<user>\AppData\Local\Packages\evoke_nh7s0b45jarrj\LocalState. For this
alpha version the files must be placed there manually after app installation.
