using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosPhiCalcTest;
internal class FormType<T> : FormType
where T : Form
{
    public override Type Type { get; }

	public FormType() : base(typeof(T)) => Type = typeof(T);

    public virtual T CreateInstance<T>() => Activator.CreateInstance<T>();
}

internal class FormType
{
    public virtual Type Type { get; }

    public FormType(Type type) => Type = type;

    public virtual Form CreateInstance() => (Form)Activator.CreateInstance(Type);

    public static implicit operator Type(FormType ftype) => ftype.Type;
}
