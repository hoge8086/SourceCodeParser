
//関数A
//戻り値なし
void funcA()
{
	// ▼修正行
	return 0;
	// ▲修正行

}
// ▼修正行

/* 追加関数B
*/
int funcB(int a)
{
	int b;
	return a + b; //修正行
}

/* 追加関数C
*/
int funcC(int a)
{
	// ▼修正行
	foreach(int b in a)
	{
		if (a > 0)
			return 100;
	}
	return 0;
	// ▲修正行
}

//関数D
//追加関数
int funcD(string d)
{
	return 0;
}
// ▲修正行

//関数E
//未修整なので検出されないはず
int funcE(string d)
{
	return 0;
}

//関数F
//単一修正行
int funcF(string d)
{
	return 0; //修正行 (単一修正行)
}


