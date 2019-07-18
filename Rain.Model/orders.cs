// Decompiled with JetBrains decompiler
// Type: Rain.Model.orders
// Assembly: Rain.Model, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7F3024AC-7DBA-4351-8C52-F3DF08C93221
// Assembly location: C:\Users\Felix.Ho\Desktop\page\bin\Rain.Model.dll

using System;
using System.Collections.Generic;

namespace Rain.Model
{
  [Serializable]
  public class orders
  {
    private string _order_no = string.Empty;
    private string _trade_no = string.Empty;
    private int _user_id = 0;
    private string _user_name = string.Empty;
    private int _payment_id = 0;
    private Decimal _payment_fee = new Decimal(0);
    private int _payment_status = 0;
    private int _express_id = 0;
    private string _express_no = string.Empty;
    private Decimal _express_fee = new Decimal(0);
    private int _express_status = 0;
    private string _accept_name = string.Empty;
    private string _post_code = string.Empty;
    private string _telphone = string.Empty;
    private string _mobile = string.Empty;
    private string _email = string.Empty;
    private string _area = string.Empty;
    private string _address = string.Empty;
    private string _message = string.Empty;
    private string _remark = string.Empty;
    private int _is_invoice = 0;
    private string _invoice_title = string.Empty;
    private Decimal _invoice_taxes = new Decimal(0);
    private Decimal _payable_amount = new Decimal(0);
    private Decimal _real_amount = new Decimal(0);
    private Decimal _order_amount = new Decimal(0);
    private int _point = 0;
    private int _status = 1;
    private DateTime _add_time = DateTime.Now;
    private int _id;
    private DateTime? _payment_time;
    private DateTime? _express_time;
    private DateTime? _confirm_time;
    private DateTime? _complete_time;
    private List<Rain.Model.order_goods> _order_goods;

    public int id
    {
      set
      {
        this._id = value;
      }
      get
      {
        return this._id;
      }
    }

    public string order_no
    {
      set
      {
        this._order_no = value;
      }
      get
      {
        return this._order_no;
      }
    }

    public string trade_no
    {
      set
      {
        this._trade_no = value;
      }
      get
      {
        return this._trade_no;
      }
    }

    public int user_id
    {
      set
      {
        this._user_id = value;
      }
      get
      {
        return this._user_id;
      }
    }

    public string user_name
    {
      set
      {
        this._user_name = value;
      }
      get
      {
        return this._user_name;
      }
    }

    public int payment_id
    {
      set
      {
        this._payment_id = value;
      }
      get
      {
        return this._payment_id;
      }
    }

    public Decimal payment_fee
    {
      set
      {
        this._payment_fee = value;
      }
      get
      {
        return this._payment_fee;
      }
    }

    public int payment_status
    {
      set
      {
        this._payment_status = value;
      }
      get
      {
        return this._payment_status;
      }
    }

    public DateTime? payment_time
    {
      set
      {
        this._payment_time = value;
      }
      get
      {
        return this._payment_time;
      }
    }

    public int express_id
    {
      set
      {
        this._express_id = value;
      }
      get
      {
        return this._express_id;
      }
    }

    public string express_no
    {
      set
      {
        this._express_no = value;
      }
      get
      {
        return this._express_no;
      }
    }

    public Decimal express_fee
    {
      set
      {
        this._express_fee = value;
      }
      get
      {
        return this._express_fee;
      }
    }

    public int express_status
    {
      set
      {
        this._express_status = value;
      }
      get
      {
        return this._express_status;
      }
    }

    public DateTime? express_time
    {
      set
      {
        this._express_time = value;
      }
      get
      {
        return this._express_time;
      }
    }

    public string accept_name
    {
      set
      {
        this._accept_name = value;
      }
      get
      {
        return this._accept_name;
      }
    }

    public string post_code
    {
      set
      {
        this._post_code = value;
      }
      get
      {
        return this._post_code;
      }
    }

    public string telphone
    {
      set
      {
        this._telphone = value;
      }
      get
      {
        return this._telphone;
      }
    }

    public string mobile
    {
      set
      {
        this._mobile = value;
      }
      get
      {
        return this._mobile;
      }
    }

    public string email
    {
      set
      {
        this._email = value;
      }
      get
      {
        return this._email;
      }
    }

    public string area
    {
      set
      {
        this._area = value;
      }
      get
      {
        return this._area;
      }
    }

    public string address
    {
      set
      {
        this._address = value;
      }
      get
      {
        return this._address;
      }
    }

    public string message
    {
      set
      {
        this._message = value;
      }
      get
      {
        return this._message;
      }
    }

    public string remark
    {
      set
      {
        this._remark = value;
      }
      get
      {
        return this._remark;
      }
    }

    public int is_invoice
    {
      set
      {
        this._is_invoice = value;
      }
      get
      {
        return this._is_invoice;
      }
    }

    public string invoice_title
    {
      set
      {
        this._invoice_title = value;
      }
      get
      {
        return this._invoice_title;
      }
    }

    public Decimal invoice_taxes
    {
      set
      {
        this._invoice_taxes = value;
      }
      get
      {
        return this._invoice_taxes;
      }
    }

    public Decimal payable_amount
    {
      set
      {
        this._payable_amount = value;
      }
      get
      {
        return this._payable_amount;
      }
    }

    public Decimal real_amount
    {
      set
      {
        this._real_amount = value;
      }
      get
      {
        return this._real_amount;
      }
    }

    public Decimal order_amount
    {
      set
      {
        this._order_amount = value;
      }
      get
      {
        return this._order_amount;
      }
    }

    public int point
    {
      set
      {
        this._point = value;
      }
      get
      {
        return this._point;
      }
    }

    public int status
    {
      set
      {
        this._status = value;
      }
      get
      {
        return this._status;
      }
    }

    public DateTime add_time
    {
      set
      {
        this._add_time = value;
      }
      get
      {
        return this._add_time;
      }
    }

    public DateTime? confirm_time
    {
      set
      {
        this._confirm_time = value;
      }
      get
      {
        return this._confirm_time;
      }
    }

    public DateTime? complete_time
    {
      set
      {
        this._complete_time = value;
      }
      get
      {
        return this._complete_time;
      }
    }

    public List<Rain.Model.order_goods> order_goods
    {
      set
      {
        this._order_goods = value;
      }
      get
      {
        return this._order_goods;
      }
    }
  }
}
