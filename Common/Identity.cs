using System;

namespace Common
{
  public abstract class Identity
  {
    public Guid Id { get; set; }

    public FullName FullName{get;set;}
  }
}