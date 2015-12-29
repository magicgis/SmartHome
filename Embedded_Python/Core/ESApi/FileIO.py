import os


def root_directory() -> str:
    filePath = os.path.realpath(__file__)
    fileDir = os.path.dirname(filePath)
    rootDir = os.path.join(fileDir, "../..");
    rootDir = os.path.realpath(rootDir)
    return rootDir

def apps_directory() -> str:
    return os.path.join(root_directory(), "Apps")


def data_directory() -> str:
    return os.path.join(root_directory(), "Data")


def sysdata_directory() -> str:
    return os.path.join(data_directory(), "SysData")


def appdata_directory() -> str:
    return os.path.join(data_directory(), "AppData")

def settings_directory() -> str:
    return os.path.join(sysdata_directory(), "settings")