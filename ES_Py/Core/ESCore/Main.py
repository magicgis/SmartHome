#!/usr/bin/python
#Main file containing startup code

import sys
import FileIO as FileIO

sys.path.append(FileIO.core_directory())

from ESCore.ApplicationManager import ApplicationManager
from ESCore.KivyMain import KivyMain

appManager = ApplicationManager()

FileIO.create_directories()
appManager.load_applications(FileIO.apps_directory())

app = KivyMain()
app.run()