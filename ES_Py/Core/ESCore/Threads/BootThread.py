import time

import kivy.clock as clock

from ESApi.Thread import Thread

import ESCore.Controller.AppLauncherController as AppLauncherController
import ESCore.ApplicationManager as ApplicationManager
import ESCore.CoreFileIO as CoreFileIO

from ESApi.IXPFile import IXPFile


class BootThread(Thread):
    def _run(self):
        clock.ClockBase.schedule_once(clock.Clock, self._init_app_launcher)

        ApplicationManager.instance.load_applications(CoreFileIO.apps_directory())

        for index in range(0, ApplicationManager.instance.application_count()):
            ApplicationManager.instance.application_at(index).app().on_system_boot()

    def _init_app_launcher(self, dt: int = 0):
        AppLauncherController.instance.initialize()