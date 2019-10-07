<Query Kind="Statements">
  <Connection>
    <ID>f3d045eb-c1ac-4bb2-8090-e42569bfb64a</ID>
    <Persist>true</Persist>
    <Server>iba-v3.cokorimskt9k.eu-west-1.rds.amazonaws.com</Server>
    <SqlSecurity>true</SqlSecurity>
    <UserName>ibaUser</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAhojkFUp1P0S/BFq0pLwaeQAAAAACAAAAAAAQZgAAAAEAACAAAAAMJ7mPukG+u2MS0n3HjuIeA+iZXQAR2z0gK0ln5IOFgwAAAAAOgAAAAAIAACAAAAB6Y3wNxVIN7y375k5xk6M6K2ChMCbUXvPpemgtaNIk+hAAAAD0tLhdPyylIR7AYDvm8S6rQAAAAE8SDPwKbj64P4gGlmMt14InglrLQxByECma+z+3qhCpR9lxhRGq8QEMU2pzgLsezRJCmB02PNHP1vuMV10x6iY=</Password>
    <Database>iba</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

var delegates = Conf_delegates.Where(r=>r.Conference_id == 673).Select(t => t.Record_id);
var attendeesAllowd = _records.Where(r=>r.Status == 1 && r.Class == 1 && delegates.Contains(r.Id));
attendeesAllowd.Dump();

