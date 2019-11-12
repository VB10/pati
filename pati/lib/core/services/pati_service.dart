import 'dart:convert';

import 'package:http/http.dart' as http;

class PatiService {
  static PatiService _instance = PatiService._init();
  PatiService._init();
  static PatiService instance = _instance;

  String _baseUrl = "http://localhost:5002";

  Future<dynamic> getPatiService() async {
    final response = await http.get("$_baseUrl/pet");  
    return json.decode(response.body);
  }
}
