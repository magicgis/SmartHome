from kivy.uix.video import Video
import ESApi.FileIO as FileIO


class LoadingScreenWidget(Video):
    def __init__(self, **kwargs):
        super(LoadingScreenWidget, self).__init__(**kwargs)
        self.volume = 0
        self.state = 'pause'
        self.options = {'eos': 'loop', 'allow_stretch': 'True'}
        self.source = source=FileIO.sysdata_directory() + "/media/boot/boot_anim.mp4"

    def play(self):
        self.state = 'play'

    def pause(self):
        self.state = 'pause'

    def stop(self):
        self.state = 'stop'
        self.unload()