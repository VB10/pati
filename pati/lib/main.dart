import 'package:flutter/material.dart';
import 'package:pati/ui/map_view.dart';

void main() {
  return runApp(MyApp());
}

class MyApp extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Material App',
      home: MapView(),
    );
  }
}
