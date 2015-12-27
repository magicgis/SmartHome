#ifndef APPLICATIONMANAGER_H
#define APPLICATIONMANAGER_H

#include <vector>

#include "application.h"

namespace EmbeddedSystem{
    namespace API{

        class ApplicationManager
        {
            private:
                std::vector<Application> m_applications;

            public:
                ApplicationManager(std::vector<Application> p_applications);

                std::vector<Application> get_applications();
        };

    }
}

#endif // APPLICATIONMANAGER_H
