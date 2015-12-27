#-------------------------------------------------
#
# Project created by QtCreator 2015-12-19T23:44:47
#
#-------------------------------------------------

QT       += core gui

greaterThan(QT_MAJOR_VERSION, 4): QT += widgets

TARGET = EmbeddedSystem
TEMPLATE = app


SOURCES += main.cpp\
        mainwindow.cpp \
    API/application.cpp \
    Clock/Model/clockapplication.cpp \
    Core/Controller/mainwindowcontroller.cpp \
    API/coresystem.cpp \
    API/applicationmanager.cpp

HEADERS  += mainwindow.h \
    API/application.h \
    Clock/Model/clockapplication.h \
    Core/Controller/mainwindowcontroller.h \
    API/coresystem.h \
    API/applicationmanager.h

FORMS    += mainwindow.ui

RESOURCES += \
    Clock/clockresources.qrc
