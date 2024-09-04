#include <stdio.h>
#include <windows.h>

struct clickInterface {
    int cps;
    bool leftClick;
    bool middleClick;
    bool rightClick;
};

CRITICAL_SECTION cs;
bool isStop = false;

bool isButtonPressed() {
    if (GetAsyncKeyState(VK_LBUTTON) & 0x8000) return true;
    return false;
}

INPUT* getLeftCLick() {
    static INPUT left[2] = {0};

    left[0].type = INPUT_MOUSE;
    left[0].mi.dwFlags = MOUSEEVENTF_LEFTDOWN;
    left[0].mi.dx = 0;
    left[0].mi.dy = 0;
    left[0].mi.mouseData = 0;
    left[0].mi.dwExtraInfo = 0;

    left[1].type = INPUT_MOUSE;
    left[1].mi.dwFlags = MOUSEEVENTF_LEFTUP;
    left[1].mi.dx = 0;
    left[1].mi.dy = 0;
    left[1].mi.mouseData = 0;
    left[1].mi.dwExtraInfo = 0;

    return left;
}

INPUT* getRightCLick() {
    static INPUT right[2] = {0};

    right[0].type = INPUT_MOUSE;
    right[0].mi.dwFlags = MOUSEEVENTF_RIGHTDOWN;
    right[0].mi.dx = 0;
    right[0].mi.dy = 0;
    right[0].mi.mouseData = 0;
    right[0].mi.dwExtraInfo = 0;

    right[1].type = INPUT_MOUSE;
    right[1].mi.dwFlags = MOUSEEVENTF_RIGHTUP;
    right[1].mi.dx = 0;
    right[1].mi.dy = 0;
    right[1].mi.mouseData = 0;
    right[1].mi.dwExtraInfo = 0;

    return right;
}

INPUT* getMiddleCLick() {
    static INPUT mid[2] = {0};

    mid[0].type = INPUT_MOUSE;
    mid[0].mi.dwFlags = MOUSEEVENTF_MIDDLEDOWN;
    mid[0].mi.dx = 0;
    mid[0].mi.dy = 0;
    mid[0].mi.mouseData = 0;
    mid[0].mi.dwExtraInfo = 0;

    mid[1].type = INPUT_MOUSE;
    mid[1].mi.dwFlags = MOUSEEVENTF_MIDDLEUP;
    mid[1].mi.dx = 0;
    mid[1].mi.dy = 0;
    mid[1].mi.mouseData = 0;
    mid[1].mi.dwExtraInfo = 0;

    return mid;
}

extern "C" __declspec(dllexport) DWORD WINAPI simulateClicks(LPVOID lpParam) {
    clickInterface* params = (clickInterface*)lpParam;
    int cps = params->cps;
    bool leftClick = params->leftClick;
    bool middleClick = params->middleClick;
    bool rightClick = params->rightClick;
    int i;

    while (true) {
        EnterCriticalSection(&cs);

        if (isStop) {
            LeaveCriticalSection(&cs);
            break;
        }

        LeaveCriticalSection(&cs);

        if (isButtonPressed()) {
            INPUT* mouseLeftClick = getLeftCLick();
            INPUT* mouseMiddleClick = getMiddleCLick();
            INPUT* mouseRightClick = getRightCLick();

            for (i = 0; i < cps; i++) {
                if (leftClick == true) {
                    SendInput(2, mouseLeftClick, sizeof(INPUT));
                }

                if (rightClick == true) {
                    SendInput(2, mouseRightClick, sizeof(INPUT));
                }

                if (middleClick == true) {
                    SendInput(2, mouseMiddleClick, sizeof(INPUT));
                }
            }
            Sleep(100);
        }
        Sleep(10); // pause CPU used scheduling
    }

    delete params;
    return 0;
}

extern "C" __declspec(dllexport) void start(int cps, bool leftClick = false, bool middleClick = false, bool rightClick = false) {
    InitializeCriticalSection(&cs);
    isStop = false;

    clickInterface* params = new clickInterface;
    params->cps = cps;
    params->leftClick = leftClick;
    params->middleClick = middleClick;
    params->rightClick = rightClick;

    HANDLE threadHandle = CreateThread(NULL, 0, simulateClicks, params, 0, NULL);

    if (threadHandle == NULL) {
        printf("Error creating thread: %d\n", GetLastError());
        DeleteCriticalSection(&cs);
        return;
    }

    CloseHandle(threadHandle);
}

extern "C" __declspec(dllexport) void stop() {
    LeaveCriticalSection(&cs);
    isStop = true;
}

int main() {
    return 0;
}
