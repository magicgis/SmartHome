import os
import sys
from kivy.core.image import Image

from ESCore.CoreApplication import CoreApplication
from ESApi.Application import Application


class ApplicationManager:
    __applications = []  # type: List[CoreApplication]

    def application_count(self) -> int:
        return len(self.__applications)

    def application_at(self, index: int) -> CoreApplication:
        if index < 0 or index >= self.application_count():
            raise IndexError()

        return self.__applications[index]

    def load_applications(self, apps_dir: str):

        for top, dirs, files in os.walk(apps_dir):
            if top != apps_dir:
                continue

            for app in dirs:
               icon = self.load_icon(apps_dir + "/" + app)
               iApp = self.load_app(apps_dir + "/" + app, app)
               cApp = CoreApplication(app, icon, iApp)
               self.__applications.append(cApp)

    def load_app(self, app_dir: str, app_name: str) -> Application:
        sys.path.append(app_dir + "/src")
        module = __import__(app_name)
        type = getattr(module, app_name)
        instance = type()
        return instance


    def load_icon(self, app_dir: str) -> Image:
        iconPath = app_dir + "/Icon.png"
        icon = Image.load(iconPath)
        return icon

instance = ApplicationManager() #type: ApplicationManager