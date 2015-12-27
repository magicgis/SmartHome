#ifndef CORESYSTEM_H
#define CORESYSTEM_H

#include "applicationmanager.h"

namespace EmbeddedSystem{
    namespace API{
        class CoreSystem{
            private:
                static CoreSystem* s_instance;

            public:
                static CoreSystem* get_instance();
                static void init();

            private:
                ApplicationManager* m_application_manager;

            public:
                ApplicationManager* get_application_manager();

            private:
                CoreSystem();
        };
    }
}

#endif // CORESYSTEM_H
