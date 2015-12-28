import time

from ESApi.Thread import Thread

import ESCore.Controller.AppLauncherController as AppLauncherController
import ESCore.ApplicationManager as ApplicationManager
import ESCore.CoreFileIO as CoreFileIO

from ESApi.IXPFile import IXPFile


class BootThread(Thread):
    def _run(self):
        AppLauncherController.instance.initialize()

        ApplicationManager.instance.load_applications(CoreFileIO.apps_directory())

        for index in range(0, ApplicationManager.instance.application_count()):
            ApplicationManager.instance.application_at(index).app().on_system_boot()

        time.sleep(5)