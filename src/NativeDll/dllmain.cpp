#define WIN32_LEAN_AND_MEAN

#include <Windows.h>

BOOL WINAPI DllMain(
	HINSTANCE hModule,
	DWORD ul_reason_for_call,
	LPVOID lpReserved)
{
	switch (ul_reason_for_call)
	{
	case DLL_PROCESS_ATTACH:
		TCHAR szFileName[MAX_PATH];

		GetModuleFileName(NULL, szFileName, MAX_PATH);
		MessageBox(NULL, szFileName, L"HELLO FROM:", 0);

		break;

	case DLL_THREAD_ATTACH:
		break;

	case DLL_THREAD_DETACH:
		break;

	case DLL_PROCESS_DETACH:
		break;
	}
	return TRUE;
}