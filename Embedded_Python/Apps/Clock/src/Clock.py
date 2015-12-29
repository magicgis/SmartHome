from ESApi.Application import Application


class Clock(Application):
    def on_system_boot(self):
        print("Clock application was loaded at boot time")