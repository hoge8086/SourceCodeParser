// CppTest.cpp : アプリケーションのエントリ ポイントを定義します。
//
#include "stdafx.h"

const char(*a) = { "abc" };

void func()
{
	int a = 10;
	if(a < 10)
	{
		a++;
	}
}

int main()
{
    return 0;

	for(int i=0; i<10; func()){
		i++;
	}
}

class CManiac {
private:
    int m_iValue;
public:
    int GetiValue() {
        return m_iValue;
    }
    CManiac& operator=(int iValue) {
        m_iValue = iValue;
        return *this;
    }
};
