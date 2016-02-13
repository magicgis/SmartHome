#!/usr/bin/python
#Main file containing startup code

import sys

import CoreFileIO as CoreFileIO
from kivy.config import Config
Config.set('graphics', 'resizable', '0')
Config.set('graphics', 'width', '800')
Config.set('graphics', 'height', '480')

sys.path.append(CoreFileIO.core_directory())

from ESCore.UI.KivyMain import KivyMain

CoreFileIO.create_directories()

app = KivyMain()
app.run()