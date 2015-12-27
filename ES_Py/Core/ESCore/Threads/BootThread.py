from ESApi.Thread import Thread

import ESCore.ApplicationManager as ApplicationManager
import ESCore.CoreFileIO as CoreFileIO

class BootThread(Thread):
    def _run(self):
        ApplicationManager.instance.load_applications(CoreFileIO.apps_directory())

        for index in range(0, ApplicationManager.instance.application_count()):
            print("Found App: " + ApplicationManager.instance.application_at(index).name())

        