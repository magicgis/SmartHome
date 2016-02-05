#!/usr/bin/python
#Main file containing startup code

import sys

import CoreFileIO as CoreFileIO

sys.path.append(CoreFileIO.core_directory())

from ESCore.UI.KivyMain import KivyMain

CoreFileIO.create_directories()

app = KivyMain()
app.run()