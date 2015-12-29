import os

def root_directory() -> str:
    filePath = os.path.realpath(__file__)
    fileDir = os.path.dirname(filePath)
    rootDir = os.path.join(fileDir, "../..");
    rootDir = os.path.realpath(rootDir)
    return rootDir


def core_directory() -> str:
    return os.path.join(root_directory(), "Core")


def esapi_directory() -> str:
    return os.path.join(core_directory(), "ESApi")


def escore_directory() -> str:
    return os.path.join(core_directory(), "ESCore")


def apps_directory() -> str:
    return os.path.join(root_directory(), "Apps")


def data_directory() -> str:
    return os.path.join(root_directory(), "Data")


def sysdata_directory() -> str:
    return os.path.join(data_directory(), "SysData")


def appdata_directory() -> str:
    return os.path.join(data_directory(), "AppData")


def create_directories() -> str:
    appsDir = apps_directory()
    dataDir = data_directory()
    sysdataDir = sysdata_directory()
    appdataDir = appdata_directory()

    if not os.path.exists(appsDir):
        os.makedirs(appsDir)

    if not os.path.exists(dataDir):
        os.makedirs(dataDir)

    if not os.path.exists(sysdataDir):
        os.makedirs(sysdataDir)

    if not os.path.exists(appdataDir):
        os.makedirs(appdataDir)