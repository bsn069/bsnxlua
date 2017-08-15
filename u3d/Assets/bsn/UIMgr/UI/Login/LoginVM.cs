using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NBsn.NMVVM
{

public class LoginVM : ViewModel
{
	public readonly BindableProperty<string> Name = new BindableProperty<string>();
	public readonly BindableProperty<string> Pwd= new BindableProperty<string>(); 

	public void Login()
	{
	}
}

}
